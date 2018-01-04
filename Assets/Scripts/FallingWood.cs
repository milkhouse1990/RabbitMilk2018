using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWood : MonoBehaviour {
    public Vector2 falling_velocity;
    public Vector2 highest_position;
    public Vector2 lowest_position;
    public int time_offset;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (time_offset > 0)
            time_offset--;
        else if (time_offset==0)
        {
            transform.position = highest_position;
            GetComponent<Rigidbody2D>().velocity = falling_velocity;
            time_offset = -1;      
        }
        else if (transform.position.y < lowest_position.y)
            transform.position = highest_position;
    }
}
