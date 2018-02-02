using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListTool : MonoBehaviour
{
    public int current = 0;

    public string binid;
    private ListItems lis;

    public bool vertical;
    public bool data;
    private int length;

    private Transform ch_cursor;
    private Transform ch_list;
    private Transform ch_info;

    private Rect info_pos;

    public void InitText(ListItems plis)
    {
        lis = plis;
        ListUpdate();
    }
    void Awake()
    {
        ch_list = transform.Find("List");
        ch_cursor = ch_list.Find("cursor");
        ch_info = transform.Find("info");
    }
    // Use this for initialization
    void Start()
    {
        lis = new ListItems(binid);
        if (data)
            length = 3;
        ListUpdate();
    }

    // Update is called once per frame
    void Update()
    {

        if (vertical)
        {
            if (Input.GetButtonDown("up"))
            {
                current--;
                if (current < 0)
                    current = lis.items.Length - 1;
                //Debug.Log(current);
                if (!data)
                    ListUpdate();
            }

            if (Input.GetButtonDown("down"))
            {
                current++;
                if (current == lis.items.Length)
                    current = 0;
                //Debug.Log(current);
                if (!data)
                    ListUpdate();
            }
        }
        else
        {
            if (Input.GetButtonDown("left"))
            {
                current--;
                if (current < 0)
                    current = 1;
            }

            if (Input.GetButtonDown("right"))
            {
                current++;
                if (current == 2)
                    current = 0;
            }
        }
    }
    private void ListUpdate()
    {
        //cursor
        int space = 35;
        RectTransform rt = ch_cursor.GetComponent<RectTransform>();

        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -30, 30);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, current * space, 30);

        //list
        string dis = "";
        for (int i = 0; i < lis.items.Length; i++)
        {
            if (i == current)
                dis += "<color=magenta>" + lis.items[i].name + "</color>\n";
            else
                dis += "<color=black>" + lis.items[i].name + "</color>\n";
        }
        ch_list.GetComponent<Text>().text = dis;

        //info
        if (ch_info != null)
            ch_info.GetComponent<Text>().text = lis.items[current].info;
    }
    public int GetFocus()
    {
        return current;
    }
    public void SetFocus(int foc)
    {
        current = foc;
    }
    public void SetInfoPos(Rect p_info_pos)
    {
        info_pos = p_info_pos;
    }
    public void SetInfoAlign(TextAnchor align)
    {
        ch_info.GetComponent<Text>().alignment = align;
    }
}
