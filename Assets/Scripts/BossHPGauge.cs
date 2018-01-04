using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPGauge : MonoBehaviour
{
    public Texture2D life;
    public Texture2D tank;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnGUI()
    {
        int hp = 0;
        GameObject boss = Transform.FindObjectOfType<Boss>().gameObject;
        if (boss != null)
            hp = boss.GetComponent<Status>().GetHp();
        else
            hp = 0;        //GUI.DrawTextureWithTexCoords(new Rect(0.5f*64, 260, 32, mhp*8), hpg,new Rect(0,0,0.25f,1));
        GUI.DrawTextureWithTexCoords(new Rect(1280 - 128, 720 - 128, 128, 128), tank, new Rect(0.75f, 0, 0.25f, 1));
        GUI.DrawTextureWithTexCoords(new Rect(1280 - 128 - 8 * 32, 720 - 128, 8 * 32, 128), tank, new Rect(0.25f, 0, 32 / 64.0f, 1));
        GUI.DrawTextureWithTexCoords(new Rect(1280 - 128 - 8 * 32 - 8 * 2, 720 - 128, 8 * 2, 128), tank, new Rect(0.25f - 2 / 64.0f, 0, 2 / 64.0f, 1));
        if (hp > 32)
        {
            GUI.DrawTextureWithTexCoords(new Rect(1280 - 128 - 8 * 32, 720 - 8 * 6, 8 * 32, 8 * 5), life, new Rect(0, 0, 1, 0.5f));
            hp -= 32;
            GUI.DrawTextureWithTexCoords(new Rect(1280 - 128 - 8 * hp, 720 - 8 * 6, 8 * hp, 8 * 5), life, new Rect(0, 0.5f, hp / 32.0f, 0.5f));
        }
        else
            GUI.DrawTextureWithTexCoords(new Rect(1280 - 128 - 8 * hp, 720 - 8 * 6, 8 * hp, 8 * 5), life, new Rect(0, 0, hp / 32.0f, 0.5f));

    }
}
