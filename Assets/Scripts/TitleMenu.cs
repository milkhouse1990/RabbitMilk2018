using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    private GameObject diaryBook;
    private GameObject DataBaseMenu;

    private GameObject main_menu;

    void Awake()
    {
        main_menu = transform.Find("MainMenu").gameObject;

        diaryBook = transform.Find("DiaryBook").gameObject;
        diaryBook.SetActive(false);

        DataBaseMenu = transform.Find("DataBaseCanvas").gameObject;
        DataBaseMenu.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        //deactive
        if (!main_menu.activeInHierarchy)
        {
            if (Input.GetButtonDown("B"))
            {
                diaryBook.SetActive(false);
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
                        DataBaseMenu.SetActive(true);
                        main_menu.SetActive(false);
                        break;
                    case 1:
                        diaryBook.SetActive(true);
                        diaryBook.GetComponent<DiaryBook>().SetSaveFlag(false);
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
