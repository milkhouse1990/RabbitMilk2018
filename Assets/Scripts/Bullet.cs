using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private bool working = false;
    public void Init()
    {
        working = false;
        transform.position = new Vector3(-99,-99,-99);
        GetComponent<Rigidbody2D>().velocity=Vector3.zero;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (working)
        {
            Vector3 screenpos = Camera.main.WorldToScreenPoint(transform.position);
            if (screenpos.x < 0 || screenpos.x > 1280 || screenpos.y < 0 || screenpos.y > 720)
                Init();
        }
        else
            transform.position = Vector3.zero;
        
    }
    public bool GetWorking()
    {
        return working;
    }
    public void SetWorking(bool p_working)
    {
        working = p_working;
    }
}
