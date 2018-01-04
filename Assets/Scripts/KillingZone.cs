using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingZone : MonoBehaviour {

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
            Status p_status = other.GetComponentInParent<Status>();
            p_status.HPChange(p_status.GetHp());
            GameObject.Destroy(gameObject);
        }
    }
}
