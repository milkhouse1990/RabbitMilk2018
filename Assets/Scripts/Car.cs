using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    private int counter = 120;
    private int phase;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (phase)
        {
            case 0:
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            case 1:
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.4f);
                break;
        }
	}
    void FixedUpdate()
    {
        if (counter > 0)
            counter--;
        if (counter==0)
            switch(phase)
            {
                case 0:
                    phase++;
                    counter = 300;
                    break;
                case 1:
                    phase = 0;
                    counter = -1;
                    break;

            }
    }
}
