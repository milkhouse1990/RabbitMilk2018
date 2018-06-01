using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private Transform switch_up;
    private Transform switch_down;
    private Transform platform;
    private bool working;
    private bool up;
    public float highestHeight;
    public float lowestHeight;
    // Use this for initialization
    void Start()
    {
        switch_up = transform.Find("switch_up");
        switch_down = transform.Find("switch_down");
        platform = transform.Find("platform");
        working = false;
        up = GetComponent<Animator>().GetBool("up");
    }

    // Update is called once per frame
    void Update()
    {
        if (platform.position.y > highestHeight)
        {
            platform.position = new Vector3(platform.position.x, highestHeight, platform.position.z);
            platform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            working = false;
        }
        if (platform.position.y < lowestHeight)
        {
            platform.position = new Vector3(platform.position.x, lowestHeight, platform.position.z);
            platform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            working = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!working)
        {

            if (other.tag == "weapon")
            {
                up = !up;
                GetComponent<Animator>().SetBool("up", up);
                working = true;
                if (up)
                    platform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2f);
                else
                    platform.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2f);
            }

        }


    }
}
