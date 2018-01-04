using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private int current;
    private string[] items;
    // Use this for initialization
    void Start()
    {
        current = 0;
        items = new string[5] { "ITEM", "EQUIPMENT", "ABILITY", "CITIZEN", "SAVE & EXIT" };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("up"))
        {
            current--;
            if (current < 0)
                current = items.Length - 1;
        }
        if (Input.GetButtonDown("down"))
        {
            current++;
            if (current == items.Length)
                current = 0;
        }
        string dis = "";
        for (int i = 0; i < items.Length; i++)
        {
            if (i == current)
                dis += "<color=magenta>" + items[i] + "</color>\n";
            else
                dis += "<color=white>" + items[i] + "</color>\n";
        }
        GetComponent<Text>().text = dis;
    }
}
