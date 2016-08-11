using UnityEngine;
using UnityEngine.UI;

public class HpUi : MonoBehaviour
{
    public DamageDetection playerDamage;
    public RectTransform hpRect;
    int maxHp;
    float initialHeight;
    float initialY;

    void Start()
    {
        maxHp = playerDamage.GetPlayerHp();
        initialHeight = hpRect.sizeDelta.y;
        initialY = hpRect.anchoredPosition.y;
    }


	void Update()
    {
        float hpRatio = (float)playerDamage.GetPlayerHp() / maxHp;

        float topValue = initialHeight * (1f - hpRatio);

        Vector2 pos = hpRect.anchoredPosition;
        pos.y = initialY - topValue/2f;
        hpRect.anchoredPosition = pos;

        Vector2 size = hpRect.sizeDelta;
        size.y = initialHeight * hpRatio;
        hpRect.sizeDelta = size;
    }
}
