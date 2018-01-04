using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ready : MonoBehaviour {
    public Transform milk;
    public GUIStyle gs;
    private int timer = 0;
    private int index = 0;
    private Transform player;
    private bool b_ready=false;
	// Use this for initialization
	void Start () {
        player = Instantiate(milk, new Vector3(0.5f, 3.5f, 0), Quaternion.identity);
        player.name = "milk";
        GameObject camera=GameObject.Find("Main Camera");
        //camera.GetComponent<CameraFollow>().target = player;
        player.gameObject.GetComponent<PlatformerCharacter2D>().Move(1, false, false,false);
        player.gameObject.GetComponent<Platformer2DUserControl>().enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		switch(index)
        {
            case 1:
                player.gameObject.GetComponent<PlatformerCharacter2D>().Move(0, false, false,false);
                b_ready = true;
                break;
            case 2:
                player.gameObject.GetComponent<Platformer2DUserControl>().enabled = true;
                GameObject.Destroy(this);
                break;
                
        }
	}
    void FixedUpdate()
    {
        timer++;
        if (timer==30)
        {
            timer = 0;
            index++;
        }
    }
    void OnGUI()
    {
        if (b_ready)
            GUI.Label(new Rect(500,250, 1280, 640), "READY",gs);
    }
}
