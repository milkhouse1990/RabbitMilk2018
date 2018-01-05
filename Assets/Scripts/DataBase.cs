using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private List page;

    public Rect list_pos;
    public Rect info_pos;

    private ListTool db_list;
    public ListTool listtool;

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

        b_item[0] = GetComponent<SystemDataManager>().LoadBools("character_collection");
        b_item[1] = GetComponent<SystemDataManager>().LoadBools("setting_collection");
        b_item[2] = GetComponent<SystemDataManager>().LoadBools("ending_collection");

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
            db_list = Instantiate(listtool, transform);
            db_list.SetListPos(list_pos);
            db_list.SetInfoPos(info_pos);
            db_list.InitText(liss[labelpos]);
        }
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
            db_list.SetFocus(pos[labelpos]);
            db_list.InitText(liss[labelpos]);

            //page.SetScroll(dispos[labelpos]);
        }
        if (Input.GetButtonDown("right"))
        {
            pos[labelpos] = db_list.GetFocus();
            //dispos[labelpos] = page.GetScroll();
            labelpos++;
            if (labelpos == labels)
                labelpos = 0;
            db_list.SetFocus(pos[labelpos]);
            db_list.InitText(liss[labelpos]);

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

}
