using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MapMove : MonoBehaviour
{

    private int place = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("up"))
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        if (Input.GetButton("down"))
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        if (Input.GetButton("left"))
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        if (Input.GetButton("right"))
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        if (Input.GetButtonDown("A"))
            if (place == 1)
                SceneManager.LoadScene("Palace_demo");

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        place = 1;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        place = 0;
    }
    void OnGUI()
    {
        GUI.Label(new Rect(0, 700, 640, 20), "<color=red>↑↓←→移动 A进入</color>");
        if (place == 1)
            GUI.Label(new Rect(960, 500, 640, 40), "<color=red>CASTLE\nfairy=1/1 data=0/0</color>");
    }
}
