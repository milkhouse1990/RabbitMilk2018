using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListTool : MonoBehaviour
{
    private int[] bag = new int[50];
    private int current = 0;
    private string[] names;

    private string[] items;
    private string[] infos;

    public bool vertical;
    public bool data;
    private int length;

    public Transform cursor;
    public Transform list;
    public Transform info;

    private Transform ch_cursor;
    private Transform ch_list;
    private Transform ch_info;

    private Rect list_pos;
    private Rect info_pos;
    public void InitText(ReadList rl)
    {
        items = rl.items;
        infos = rl.infos;
        length = rl.items.Length;

        if (ch_cursor == null)
        {
            ch_cursor = Instantiate(cursor, transform);

            ch_list = Instantiate(list, transform);
            RectTransform rt = ch_list.GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, list_pos.x, list_pos.width);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, list_pos.y, list_pos.height);

            ch_info = Instantiate(info, transform);
            rt = ch_info.GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, info_pos.x, info_pos.width);
            rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, info_pos.y, info_pos.height);

        }

        ListUpdate();
    }
    // Use this for initialization
    void Start()
    {
        if (data)
            length = 3;
    }

    void Awaken()
    {
        //ch_item_name = GameObject.Find("item_name");
        //ch_cursor = GameObject.Find("cursor");
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
                    current = length - 1;
                //Debug.Log(current);
                if (!data)
                    ListUpdate();
            }

            if (Input.GetButtonDown("down"))
            {
                current++;
                if (current == length)
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

        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, list_pos.x - 30, 30);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, list_pos.y + current * space, 30);

        //list
        string dis = "";
        for (int i = 0; i < items.Length; i++)
        {
            if (i == current)
                dis += "<color=magenta>" + items[i] + "</color>\n";
            else
                dis += "<color=black>" + items[i] + "</color>\n";
        }
        ch_list.GetComponent<Text>().text = dis;

        //info
        ch_info.GetComponent<Text>().text = infos[current];
    }
    public int GetFocus()
    {
        return current;
    }
    public void SetFocus(int foc)
    {
        current = foc;
    }
    public void SetListPos(Rect p_list_pos)
    {
        list_pos = p_list_pos;
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
