using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraForcedRoll : MonoBehaviour {

    private GameObject milk;
    private Vector2 scroll_velocity = new Vector2(4f, 0);

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(10, 5.625f, -10);
        if ((milk = GameObject.Find("milk")) == null)
            Debug.Log("cant find milk");
        GetComponent<Rigidbody2D>().velocity = scroll_velocity;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < 20)
        {
            //force velocity
            milk.GetComponent<Rigidbody2D>().velocity+= new Vector2(4f,0);
        }
        else
        {
            GetComponent<CameraFollow>().enabled = true;
            enabled=false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
            
    }
}
