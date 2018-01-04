using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ACT_time : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        System.DateTime now = System.DateTime.Now;
        GetComponent<Text>().text = now.ToLongTimeString();
    }
}
