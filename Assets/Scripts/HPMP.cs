using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPMP : MonoBehaviour {
    private Status c_Status;
    private int hp, mhp, mp, mmp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject milk = GameObject.Find("milkMoon");
        if (milk != null)
        {
            if (milk.GetComponent<AvgEngine>().enabled)
                return;
            c_Status = milk.GetComponent<Status>();
            hp = c_Status.GetHp();
            mhp = c_Status.hpmax;
            mp = c_Status.GetMp();
            mmp = c_Status.mpmax;
        }
        GetComponent<Text>().text="HP " + hp.ToString() + "/" + mhp.ToString()+"\n"+"MP " + mp.ToString() + "/" + mmp.ToString();
    }
}
