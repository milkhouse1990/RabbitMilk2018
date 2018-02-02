using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWood : MonoBehaviour
{
    public Vector2 falling_velocity;
    public Vector2 highest_position;
    public Vector2 lowest_position;
    public int time_offset;
    public int type = 0;
    private Physics2DM physics2DM;
    void Awake()
    {
        physics2DM = GetComponent<Physics2DM>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time_offset > 0)
            time_offset--;
        else if (time_offset == 0)
        {
            MoveStart();
        }
        else
            MoveTrigger();
    }

    private void MoveStart()
    {
        switch (type)
        {
            case 0:
                transform.position = highest_position;
                GetComponent<Physics2DM>().velocity = falling_velocity;
                time_offset = -1;
                break;
            case 1:
                transform.position = highest_position;
                GetComponent<Physics2DM>().velocity = falling_velocity;
                time_offset = -1;
                break;
        }
    }
    private void MoveTrigger()
    {
        switch (type)
        {
            case 0:
                if (transform.position.y < lowest_position.y)
                    transform.position = highest_position;
                break;
            case 1:
                if (transform.position.y < lowest_position.y || transform.position.y > highest_position.y)
                    physics2DM.velocity = new Vector2(physics2DM.velocity.x, -physics2DM.velocity.y);
                break;
        }
    }
}