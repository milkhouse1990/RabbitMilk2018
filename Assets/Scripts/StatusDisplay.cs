using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour
{
    //private Status c_Status;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string player_name = "milkMoon";
        GameObject milk = GameObject.Find(player_name);
        if (milk != null)
        {
            Status c_status = milk.GetComponent<Status>();
            string dis = "";
            dis += "Lv 1\n";
            dis += "HP " + c_status.GetHp().ToString() + "/" + c_status.hpmax.ToString() + "\n";
            dis += "MP " + c_status.GetHp().ToString() + "/" + c_status.hpmax.ToString() + "\n";
            dis += "ATK " + c_status.atk.ToString() + "\n";
            dis += "DEF " + c_status.def.ToString() + "\n";
            dis += "EXP " + "0" + "\n";
            dis += "NEXT " + "10" + "\n";
            GetComponent<Text>().text = dis;
        }

    }
}
