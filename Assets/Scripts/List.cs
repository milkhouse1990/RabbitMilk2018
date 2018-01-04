using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class List : MonoBehaviour
{

    private int item = 0;
    private int delay = 0;
    private string pre_items;
    private string pre_infos;
    private string[] items;
    private string[] infos;
    private Texture2D vector;
    private bool b_info = true;

    private Rect pos;
    private Rect info_pos;
    public int vspace;
    public Texture2D bg;


    private int itemperscr = 10;
    private int dispos = 0;

    public void Init(Rect p_pos, Rect p_info_pos, Texture2D p_vector)
    {
        pos = p_pos;
        info_pos = p_info_pos;
        vector = p_vector;
    }
    public void InitText(string p_pre_items, string p_pre_infos)
    {
        pre_items = p_pre_items;
        items = pre_items.Split('\n');

        infos = new string[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            int point = p_pre_infos.LastIndexOf("\r\n\r\n");
            infos[items.Length - 1 - i] = p_pre_infos.Substring(point + 4);
            p_pre_infos = p_pre_infos.Substring(0, point);
            //Debug.Log(infos[items.Length - 1 - i]);
        }
        //pre_infos = p_pre_infos;
        //infos = pre_infos.Split('\n');
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        {
            if (Input.GetButtonDown("down") ||
            (Input.GetButton("down") && delay == 0))
            {

                delay = 10;
                item++;
                if (item - dispos == itemperscr)
                    dispos++;
                if (item == items.Length)
                {
                    item = 0;
                    dispos = 0;
                }
            }
            if (Input.GetButtonDown("up") ||
                (Input.GetButton("up") && delay == 0))
            {
                delay = 10;
                item--;
                if (item < dispos)
                    dispos--;
                if (item == -1)
                {
                    item = items.Length - 1;
                    if ((dispos = item - itemperscr + 1) < 0)
                        dispos = 0;
                }
            }
        }

    }
    void FixedUpdate()
    {
        if (delay > 0)
            delay--;
    }

    public void Display()
    {
        //items

        //background
        GUI.DrawTextureWithTexCoords(new Rect(pos.x - 20, pos.y - 20, pos.width + 40, pos.height * itemperscr + 40), bg, new Rect(1, 1, 1, 1));
        //text
        string dis = "";
        for (int i = dispos; i < Mathf.Min(dispos + itemperscr, items.Length); i++)
        {
            if (i == item)
                dis = "<color=magenta>" + items[i] + "</color>";
            else
                dis = "<color=black>" + items[i] + "</color>";
            GUI.Label(new Rect(pos.x, pos.y + (i - dispos) * vspace, pos.width, pos.width), dis);
        }
        //vector
        GUI.Label(new Rect(pos.x - 20, pos.y + (item - dispos) * vspace, 20, 20), vector);

        //info

        //background
        GUI.DrawTextureWithTexCoords(new Rect(info_pos.x - 20, info_pos.y - 20, info_pos.width + 40, info_pos.height + 40), bg, new Rect(1, 1, 1, 1));

        if (b_info)
        {
            dis = "<color=black>" + infos[item] + "</color>";
            GUI.Label(info_pos, dis);
        }
    }

    public int GetFocus()
    {
        return item;
    }
    public void SetFocus(int p_focus)
    {
        item = p_focus;
    }
    public int GetScroll()
    {
        return dispos;
    }
    public void SetScroll(int p_scroll)
    {
        dispos = p_scroll;
    }
    public bool GetBInfo()
    {
        return b_info;
    }
    public void SetBInfo(bool p_b_info)
    {
        b_info = p_b_info;
    }
}
