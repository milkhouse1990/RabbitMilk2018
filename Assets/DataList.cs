using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataList : MonoBehaviour
{
    public int current = 0;
    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int dataCompacity = 3;
        if (Input.GetButtonDown("up"))
        {
            current--;
            if (current < 0)
                current = dataCompacity - 1;
        }

        if (Input.GetButtonDown("down"))
        {
            current++;
            if (current == dataCompacity)
                current = 0;
        }
    }
}
