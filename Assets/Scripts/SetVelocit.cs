using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetVelocit : MonoBehaviour
{
    public Vector2 vel;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = vel;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 2)
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
