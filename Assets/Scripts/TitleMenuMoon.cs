using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuMoon : MonoBehaviour
{

    //public List list;
    //public Texture2D vector;

    //public Rect list_pos;
    public Rect info_pos;
    public Rect title_pos;

    private List page;

    private string info;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("START"))
        {
            SceneManager.LoadScene("Milkhouse");
        }


    }
    void OnGUI()
    {
        GUI.Label(title_pos, "Moon Story Ver. 201709");
        GUI.Label(info_pos, "PRESS START BUTTON");
        page.Display();
    }

}
