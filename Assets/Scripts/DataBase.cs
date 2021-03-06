﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    public Texture2D[] tachie;

    private int itemperscr = 10;
    private int delay = 0;
    private string[] labelname = { "Characters", "Enemies", "Endings" };
    private string binid;
    private bool[][] b_item;

    private int[] pos = { 0, 0, 0 };
    private int[] dispos = { 0, 0, 0 };
    private int labelpos = 0;
    private int cospos = 0;
    private int labels;
    private bool b_info = true;
    ListItems[] liss;

    private ListTool db_list;

    private Text category;
    private string[] categoryNames = { "Characters", "Expressions", "Endings" };
    void Awake()
    {
        category = transform.Find("Category").GetComponent<Text>();
    }
    // Use this for initialization
    void Start()
    {
        labels = labelname.Length;

        b_item = new bool[labels][];

        liss = new ListItems[labels];
        for (int i = 0; i < labels; i++)
        {
            int j = i + 1;
            binid = "MENU000" + j.ToString();
            liss[i] = new ListItems(binid);
        }

        SystemDataManager systemDataManager = new SystemDataManager();
        b_item[0] = systemDataManager.LoadBools("character_collection");
        b_item[1] = systemDataManager.LoadBools("setting_collection");
        b_item[2] = systemDataManager.LoadBools("ending_collection");

        //message lock manage    

        for (int i = 0; i < liss.Length; i++)
        {
            if (b_item[i].Length != liss[i].items.Length)
                Debug.Log("savedata wrong.");

            for (int j = 0; j < liss[i].items.Length; j++)
            {
                if (!b_item[i][j])
                {
                    liss[i].items[j].name = "???";
                    liss[i].items[j].info = "???????";
                }
            }
        }

        {
            db_list = transform.Find("ListTool").GetComponent<ListTool>();
            db_list.InitText(liss[labelpos]);
        }

        UpdateMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("left"))
        {
            pos[labelpos] = db_list.GetFocus();
            //dispos[labelpos] = page.GetScroll();
            labelpos--;
            if (labelpos == -1)
                labelpos = labels - 1;

            UpdateMenu();
            //page.SetScroll(dispos[labelpos]);
        }
        if (Input.GetButtonDown("right"))
        {
            pos[labelpos] = db_list.GetFocus();
            //dispos[labelpos] = page.GetScroll();
            labelpos++;
            if (labelpos == labels)
                labelpos = 0;

            UpdateMenu();
            //page.SetScroll(dispos[labelpos]);
        }
        if (Input.GetButtonDown("A"))
            //page.SetBInfo(!page.GetBInfo());
            if (Input.GetButtonDown("X"))
                cospos++;
        if (Input.GetButtonDown("B"))
            SceneManager.LoadScene("Title");
    }
    void FixedUpdate()
    {
        if (delay > 0)
            delay--;
    }

    private void UpdateMenu()
    {
        db_list.SetFocus(pos[labelpos]);
        db_list.InitText(liss[labelpos]);
        category.text = categoryNames[labelpos];
    }
}
