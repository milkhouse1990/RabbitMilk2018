using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GotoPlot : MonoBehaviour {
    public string plotno;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            string p = PlayerPrefs.GetString("Plot", "000");
            if (ComparePlot(p,plotno))
            {
                string binid = "PLOT" + plotno;
                other.transform.parent.GetComponent<Platformer2DUserControl>().EnterAVGMode(binid);
                GameObject.Destroy(gameObject);
                PlayerPrefs.SetString("Plot", plotno);
            }
            
        }
    }
    private bool ComparePlot(string proc,string ev){
        if (proc[0] > ev[0])
            return false;
        else if (proc[0] < ev[0])
            return true;
        else if (proc[1] > ev[1])
            return false;
        else if (proc[1] < ev[1])
            return true;
        else if (proc[2] < ev[2])
            return true;
        else
            return false;
    }
}
