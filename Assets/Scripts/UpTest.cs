using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpTest : MonoBehaviour
{
    private bool OnRamp = false;
    private bool OnRampTemp = false;
    private float move = 0f;
    // Use this for initialization
    void Start()
    {
        // GetComponent<Rigidbody2D>().velocity = new Vector2(4f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        move = 0;
        if (Input.GetButton("right"))
            move = 4f;
        if (Input.GetButton("left"))
            move = -4f;
        if (OnRamp)
        {
            // move = move / 2;
            if (move > 0)
                GetComponent<Rigidbody2D>().velocity = new Vector2(move, 0);
            else
                GetComponent<Rigidbody2D>().velocity = new Vector2(move, move);


        }
        else
            GetComponent<Rigidbody2D>().velocity = new Vector2(move, GetComponent<Rigidbody2D>().velocity.y);

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // if (OnRampTemp)
        //     OnRampTemp = false;
        // else
        {
            OnRamp = true;
            GetComponent<Rigidbody2D>().gravityScale = 0f;


        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // if (!OnRampTemp)
        //     OnRampTemp = true;
        // else
        {
            OnRamp = false;
            GetComponent<Rigidbody2D>().gravityScale = 1f;

        }
    }
}
