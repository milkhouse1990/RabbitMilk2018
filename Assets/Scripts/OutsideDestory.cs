using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideDestory : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 screenpos = Camera.main.WorldToScreenPoint(transform.position);
        if (screenpos.x < 0 || screenpos.x > 1280 || screenpos.y < 0 || screenpos.y > 720)
            GameObject.Destroy(gameObject);
		
	}
}
