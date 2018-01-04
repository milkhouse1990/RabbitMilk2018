using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLeft : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        transform.position -= new Vector3(speed, 0, 0);
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        if (pos.x < -640)
        {
            pos += new Vector3(2 * 1280, 0,0);
            transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
            
    }
}
