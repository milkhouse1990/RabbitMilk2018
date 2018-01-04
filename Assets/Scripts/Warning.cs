using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour {
    public GUIStyle gs;
    private CameraFollow c_cf;
    private GameObject player;
    private int timer = 0;
    private int index = 0;
    public GameObject boss;
    public Transform bosshp;
	// Use this for initialization
	void Start () {
        
        player = GameObject.Find("milk");

        
    }
	
	// Update is called once per frame
	void Update () {
        player.gameObject.GetComponent<Platformer2DUserControl>().enabled = false;
        switch (index)
        {
            case 1:
                break;
            case 2:
                player.gameObject.GetComponent<Platformer2DUserControl>().enabled = true;
                GameObject bossi=Instantiate(boss, new Vector3(100, 8, 0), Quaternion.identity);
                //Instantiate(bosshp, new Vector3(100, 8, 0), Quaternion.identity);
                //bossi.name = "boss";
                GameObject.Destroy(gameObject);
                index++;
                break;

        }


	}
    void FixedUpdate()
    {
        timer++;
        if (timer == 30)
        {
            timer = 0;
            index++;
        }
    }
    void OnGUI()
    {
        if (index==1)
            GUI.Label(new Rect(500, 250, 1280, 640), "WARNING", gs);
    }
}
