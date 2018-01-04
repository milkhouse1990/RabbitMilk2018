using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss0BerryArm : MonoBehaviour {
    private bool backwards = false;
    private int skill = -1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
		
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.parent.GetComponent<Boss0Berry>().SetBackwards(true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
            
        //backwards = true;
    }
    public void SetSkill(int p_skill)
    {
        skill = p_skill;
    }
}
