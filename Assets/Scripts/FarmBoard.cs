using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmBoard : MonoBehaviour
{
    private bool pause = false;
    private bool m_waitnpc = false;
    private PlatformerCharacter2D milk;
    public Texture2D waitnpc;
    private GameObject farm_menu;
    private string player_name;
    public GameObject popup_menu;
    // Use this for initialization
    void Start()
    {
        farm_menu = Instantiate(popup_menu);
        farm_menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_waitnpc)
        {
            if (pause)
            {
                if (Input.GetButtonDown("B"))
                {
                    if (player_name == "milk")
                        milk.GetComponent<Platformer2DUserControl>().SetPause(false);
                    else
                        milk.GetComponent<Platformer2DUserControlMoon>().SetPause(false);
                    pause = false;
                    farm_menu.SetActive(false);
                }

            }
            else
            {
                if (Input.GetButtonDown("up"))
                {
                    if (player_name == "milk")
                        milk.GetComponent<Platformer2DUserControl>().SetPause(true);
                    else
                        milk.GetComponent<Platformer2DUserControlMoon>().SetPause(true);
                    pause = true;
                    PlayerPrefs.SetInt("SaveFlag", 1);
                    farm_menu.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.name);
        switch (other.gameObject.tag)
        {
            case "Player":
                milk = other.GetComponentInParent<PlatformerCharacter2D>();
                player_name = other.transform.parent.name;
                m_waitnpc = true;
                break;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(other.name);
        switch (other.gameObject.tag)
        {
            case "Player":
                milk = null;
                m_waitnpc = false;
                break;
        }
    }
    void OnGUI()
    {
        if (m_waitnpc)
            if (milk.GetGround())
            {
                Vector3 screenpos = Camera.main.WorldToScreenPoint(milk.transform.position);
                GUI.Label(new Rect(screenpos.x - 24, screenpos.y - 128, 64, 64), waitnpc);
            }

    }
}
