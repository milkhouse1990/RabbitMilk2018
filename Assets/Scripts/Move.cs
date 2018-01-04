using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    int timer = -1;
    int alarm = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (timer > 0)
            timer--;
        else if (timer == 0)
        {
            timer = alarm;

            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, 0);
            if (transform.position.y <= 2.5f)
            {
                transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
                timer = -1;
                GameObject milk = GameObject.Find("milk");
                milk.GetComponent<AvgEngine>().Resume();
            }

        }
    }

    public void Downstairs()
    {
        alarm = (int)(0.4f * 60);
        timer = alarm;
    }
}
