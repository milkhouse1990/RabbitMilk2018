using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{

    //private GameObject title_menu;

    private string info;

    public GameObject data_canvas;
    private GameObject farm_menu;
    public GameObject DataBaseCanvas;
    private GameObject DataBaseMenu;
    private bool pause = false;

    private GameObject main_menu;

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
        main_menu = transform.Find("MainMenu").gameObject;
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
                main_menu.SetActive(true);
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
                        main_menu.SetActive(false);
                        break;
                    case 1:
                        pause = true;
                        farm_menu.SetActive(true);
                        main_menu.SetActive(false);
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
