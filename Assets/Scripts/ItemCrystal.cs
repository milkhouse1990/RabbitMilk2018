using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCrystal : MonoBehaviour {

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
            int crystal = PlayerPrefs.GetInt("Crystal", 0) + 1;
            if (crystal > 999)
                crystal = 999;
            PlayerPrefs.SetInt("Crystal",crystal);
            GameObject.Destroy(gameObject);
        }
    }
}
