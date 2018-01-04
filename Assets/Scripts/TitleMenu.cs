using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{

    private List page;
    //private GameObject title_menu;

    private string info;

    public GameObject data_canvas;
    private GameObject farm_menu;
    public GameObject DataBaseCanvas;
    private GameObject DataBaseMenu;
    private bool pause = false;

    public ListTool list_tool;
    private ListTool main_menu;
    public Rect list_pos;
    public Rect info_pos;

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.DeleteAll();

        farm_menu = Instantiate(data_canvas);
        farm_menu.GetComponentInChildren<GameDataManager>().save_flag = false;
        farm_menu.SetActive(false);

        DataBaseMenu = Instantiate(DataBaseCanvas);
        DataBaseMenu.SetActive(false);

        //list
        string binid = "MENU0000";
        ReadList rl = new ReadList(binid);

        main_menu = Instantiate(list_tool, transform);
        main_menu.SetListPos(list_pos);
        main_menu.SetInfoPos(info_pos);
        main_menu.GetComponent<ListTool>().InitText(rl);
        main_menu.SetInfoAlign(TextAnchor.MiddleCenter);
    }

    // Update is called once per frame
    void Update()
    {
        //deactive
        if (pause)
        {
            if (Input.GetButtonDown("B"))
            {
                pause = false;
                farm_menu.SetActive(false);
                DataBaseMenu.SetActive(false);
                main_menu.gameObject.SetActive(true);
            }

        }
        else
        {
            //operation
            if (Input.GetButtonDown("A"))
            {
                switch (main_menu.GetComponent<ListTool>().GetFocus())
                {
                    case 0:
                        if (Input.GetButton("L"))
                            SceneManager.LoadScene("School");
                        else
                        {
                            DataInit();
                            SceneManager.LoadScene("ACT");
                        }
                        break;
                    case 2:
                        pause = true;
                        DataBaseMenu.SetActive(true);
                        main_menu.gameObject.SetActive(false);
                        break;
                    case 1:
                        pause = true;
                        farm_menu.SetActive(true);
                        main_menu.gameObject.SetActive(false);
                        break;
                    case 4:
                        PlayerPrefs.DeleteAll();
                        Application.Quit();
                        break;
                    case 3:
                        // SceneManager.LoadScene("Milkhouse");
                        break;
                }

            }
        }

    }
    void DataInit()
    {
        //crystal
        PlayerPrefs.SetInt("Crystal", 0);
        //fairy
        PlayerPrefs.SetInt("Fairy", 0);
        // scene name
        PlayerPrefs.SetString("SceneName", "0Castle0Party");
    }
}
