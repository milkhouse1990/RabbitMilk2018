using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palace : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "weapon")
        {
            Debug.Log(gameObject.name);
            GameObject milk = GameObject.Find("milk");
            milk.GetComponent<Platformer2DUserControl>().EnterAVGMode("PLOT0001");
        }
}
}
