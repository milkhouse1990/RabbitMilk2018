using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diag : MonoBehaviour {
    public Text text;
    public ListTool listtool;

    private Text alarm;
    public Rect alarm_pos;
    public Rect selection_pos;
    private Text confirm_button;
	// Use this for initialization
	void Awake () {
        alarm = Instantiate(text, transform);
        alarm.GetComponent<TextSetPos>().SetPos(alarm_pos);
        string binid = "DIAG000";
        ReadList rl = new ReadList(binid);
        string alarm_text = rl.items[0] + "\n" + rl.items[1];
        alarm.text = alarm_text;

        confirm_button = Instantiate(text, transform);
        confirm_button.GetComponent<TextSetPos>().SetPos(selection_pos);
        string selection_text = rl.infos[0] + "      " + rl.infos[1];
        confirm_button.text = selection_text;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
