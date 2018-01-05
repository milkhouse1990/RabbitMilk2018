using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diag : MonoBehaviour
{
    public Text text;
    public ListTool listtool;

    private Text alarm;
    public Rect alarm_pos;
    public Rect selection_pos;
    private Text confirm_button;
    // Use this for initialization
    void Awake()
    {
        alarm = Instantiate(text, transform);
        alarm.GetComponent<TextSetPos>().SetPos(alarm_pos);
        string binid = "DIAG000";
        ListItems list = new ListItems(binid);
        string alarm_text = list.items[0].name + "\n" + list.items[1].name;
        alarm.text = alarm_text;

        confirm_button = Instantiate(text, transform);
        confirm_button.GetComponent<TextSetPos>().SetPos(selection_pos);
        string selection_text = list.items[0].info + "      " + list.items[1].info;
        confirm_button.text = selection_text;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
