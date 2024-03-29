﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HackKeySystem : MonoBehaviour
{
	public CharacterCombat combat;
    public Text comboText;
    public AudioSource rhythmAudio;
    public AudioSource hitAudio;

    public Image aCircleImage;
    public Image sCircleImage;
    public Image dCircleImage;

    List<HackKeyController> hackKeys;
    const float hackKeySpawnPeriod = 0.5f;
    float hackKeySpawnTime;
    int spawnIndex;
    Random random;
    int comboCount;
    const float CIRCLE_FADE_SPEED = 2f;

    const float targetY = -200f;
    const float targetHitRange = 25f;

    void Awake()
    {
        hackKeys = new List<HackKeyController>();
    }

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnHackKey", hackKeySpawnPeriod, hackKeySpawnPeriod);
        comboText.gameObject.SetActive(false);
	}
	
	void Update()
    {
        char inputChar = 'X';
        if (Input.GetKeyDown(KeyCode.A))
        {
            inputChar = 'A';
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            inputChar = 'S';
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inputChar = 'D';
        }
        if (inputChar != 'X')
        {
            bool isSuccess = false;
            foreach (HackKeyController key in hackKeys)
            {
                if (key.gameObject.activeInHierarchy && key.keyChar == inputChar)
                {
                    float distanceToTarget = Mathf.Abs(key.GetY() - targetY);
                    if (distanceToTarget < targetHitRange)
                    {
                        isSuccess = true;
                        comboCount++;
                        comboText.gameObject.SetActive(true);
                        key.Hit();
                        hitAudio.Play();
                        OnKeyHit(inputChar);
                        combat.RangeAttack();
                        break;
                    }
                }
            }
            if (!isSuccess)
            {
                comboCount = 0;
                comboText.gameObject.SetActive(false);
            }
            comboText.text = "x" + comboCount.ToString();
        }

        DecrementCircleAlpha(aCircleImage);
        DecrementCircleAlpha(sCircleImage);
        DecrementCircleAlpha(dCircleImage);
    }

    public void AddHackKey(HackKeyController hackKey)
    {
        hackKeys.Add(hackKey);
    }

    void SpawnHackKey()
    {
        hackKeys[spawnIndex++].Spawn();
        spawnIndex %= hackKeys.Count;
    }

    public void SoundRhythm()
    {
        rhythmAudio.Play();
    }

    public void OnKeyHit(char key)
    {
        switch (key)
        {
            case 'A':
                SetCircleAlpha(aCircleImage, 1.0f);
                break;
            case 'S':
                SetCircleAlpha(sCircleImage, 1.0f);
                break;
            case 'D':
                SetCircleAlpha(dCircleImage, 1.0f);
                break;
        }
    }

    void SetCircleAlpha(Image circle, float aValue)
    {
        Color color = circle.color;
        color.a = aValue;
        circle.color = color;
    }

    void DecrementCircleAlpha(Image circle)
    {
        Color color = circle.color;
        color.a = Mathf.Max(color.a - CIRCLE_FADE_SPEED * Time.deltaTime, 0);
        circle.color = color;
    }
}
