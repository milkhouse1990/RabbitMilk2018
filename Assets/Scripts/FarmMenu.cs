using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmMenu : MonoBehaviour
{
    public Texture2D bg;
    public Texture2D field;
    public Texture2D square;
    public Texture2D cursor;

    private int[] bag = new int[50];

    private int current = 0;
    private string[] names;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 50; i++)
            bag[i] = 0;
        bag[0] = 1;
        bag[1] = 2;

        names = new string[2] { "[农具/种]胡萝卜的种子", "[农具/壶]破破烂烂的水壶" };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("B"))
        {
            GameObject milk = GameObject.Find("milk");
            milk.GetComponent<Platformer2DUserControl>().SetPause(false);
            enabled = false;
            GetComponent<FarmBoard>().enabled = true;
        }
        if (Input.GetButtonDown("left"))
            current--;
        if (Input.GetButtonDown("right"))
            current++;
        if (Input.GetButtonDown("A"))
        {
            //GetComponent<GameDataManager>().Save();
        }
    }
    void OnGUI()
    {
        //bg
        GUI.DrawTextureWithTexCoords(new Rect(100, 100, 1080, 520), bg, new Rect(1, 1, 1, 1));

        GUI.Label(new Rect(200, 380, 1280, 360), "<color=black>EMPTY" + "</color>");
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                GUI.Label(new Rect(160 + i * 64, 160 + j * 64, 128, 128), field);

        string dis;
        if (bag[current] == 0)
            dis = "";
        else
            dis = names[bag[current] - 1];

        GUI.Label(new Rect(600, 500, 200, 200), "<color=black>" + dis + "</color>");
        for (int i = 0; i < 10; i++)
            GUI.Label(new Rect(160 + i * 64, 550, 64, 64), square);
        GUI.Label(new Rect(160 - 4 + current * 64, 550 - 4, 72, 72), cursor);
    }
}
