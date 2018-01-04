using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    float timer = 0;
    float direction = -0.1f;
    bool isOnMovingPlatform = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(Vector3.right * direction);
        if (timer > 1)
        {
            direction *= -1;
            timer = 0;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            isOnMovingPlatform = true;
            other.transform.parent = transform;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            if (isOnMovingPlatform)
            {
                other.transform.parent = null;
                isOnMovingPlatform = false;
            }
        }
    }
}
