using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpGuage : MonoBehaviour
{
    public int hp = 64;
    private Slider slider;
    public Sprite lifeTexture0;
    public Sprite lifeTexture1;
    public Sprite lifeTexture2;
    private Image backImage;
    private Image fillImage;

    private Boss boss;
    void Awake()
    {
        slider = GetComponent<Slider>();
        backImage = transform.Find("Background").GetComponent<Image>();
        fillImage = slider.fillRect.GetComponent<Image>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        boss = FindObjectOfType<Boss>();
        if (boss)
            hp = boss.GetComponent<Status>().GetHp();
        else
            hp = 0;
        if (hp > 32)
        {
            transform.Find("Background").GetComponent<Image>().sprite = lifeTexture1;
            slider.fillRect.GetComponent<Image>().sprite = lifeTexture2;
            slider.value = hp - 32;
        }
        else
        {
            backImage.sprite = lifeTexture0;
            fillImage.sprite = lifeTexture1;
            slider.value = hp;
        }
    }
}
