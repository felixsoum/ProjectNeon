using UnityEngine;
using UnityEngine.UI;

public class HackKeyController : MonoBehaviour
{
    public HackKeySystem system;
    Text keyText;
    const float MOVE_SPEED = 200f;
    const float SPAWN_Y = 240f;
    const float UNSPAWN_Y = -240f;
    const float TARGET_Y = -200f;
    public char keyChar = 'X';

    const float A_POS_X= -53.33f;
    const float S_POS_X = 0;
    const float D_POS_X = 53.33f;
    bool hasPlayedRhythmSound;

	void Start()
    {
        keyText = GetComponent<Text>();
        system.AddHackKey(this);
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update()
    {
        Vector2 pos = keyText.rectTransform.localPosition;
        pos.y -= MOVE_SPEED * Time.deltaTime;
        keyText.rectTransform.localPosition = pos;

        if (!hasPlayedRhythmSound && pos.y <= TARGET_Y)
        {
            hasPlayedRhythmSound = true;
            system.SoundRhythm();
        }

        if (pos.y <= UNSPAWN_Y)
        {
            gameObject.SetActive(false);
        }
	}

    public void Spawn()
    {
        hasPlayedRhythmSound = false;
        int randomInt = Random.Range(0, 3);
        float posX;
        switch (randomInt)
        {
            case 0:
                keyChar = 'A';
                posX = A_POS_X;
                break;
            case 1:
                keyChar = 'S';
                posX = S_POS_X;
                break;
            case 2:
            default:
                keyChar = 'D';
                posX = D_POS_X;
                break;
        }
        gameObject.SetActive(true);
        Vector2 pos = keyText.rectTransform.localPosition;
        pos.x = posX;
        pos.y = SPAWN_Y;
        keyText.rectTransform.localPosition = pos;
        keyText.text = keyChar.ToString();
    }

    public float GetY()
    {
        return keyText.rectTransform.localPosition.y;
    }

    public void Hit()
    {
        keyText.text = "";
    }
}
