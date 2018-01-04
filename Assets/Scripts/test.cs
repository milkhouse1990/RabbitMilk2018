using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int x = 2;
        transform.position = new Vector3(x, 2, 0);
        if (transform.position.x == x)
            Debug.Log("ok");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
