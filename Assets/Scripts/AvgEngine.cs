using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AvgEngine : MonoBehaviour
{

    public GameObject canvas;
    private GameObject co_canvas;
    public Texture2D icon;
    public Texture2D icon_milk;
    public Texture2D frame;
    public Texture2D frame1;
    public Texture2D np;

    /*
    0 nextscene
    1 hime
    */
    private GameObject npc;

    //ifstream file;
    private Line[] commands;
    private string[] para;
    private int i = 0;
    private string speaker_name = "";

    private string words = "";
    private bool pause = false;
    private bool wait = false;
    private int alarm = -1;

    private const int FPS = 60;

    //for "warning"
    private bool warning_logo;
    private bool can_skip;
    private GameObject act_init;
    // Use this for initialization
    void OnEnable()
    {
        warning_logo = false;
        can_skip = true;
        co_canvas.SetActive(true);
    }
    void Awake()
    {
        co_canvas = Instantiate(canvas);
        co_canvas.name = "AVGCanvas";
        co_canvas.SetActive(false);
        act_init = GameObject.Find("ACTInit");
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause && !wait)
        {
            //command load
            if (i < commands.Length && !wait && !pause)
            {
                //Debug.Log(commands[i]);
                //command analysis
                //int i = 0;
                para = commands[i].args;
                //command understanding
                switch (para[0])
                {
                    //pause time(s)
                    case "pause":
                        wait = true;
                        int temp;
                        int.TryParse(para[1], out temp);
                        alarm = temp * FPS;
                        break;

                    //create category name x y
                    case "create":
                        string category = para[1];
                        string name = para[2];
                        float x, y;
                        float.TryParse(para[3], out x);
                        float.TryParse(para[4], out y);
                        //check whether the npc exists
                        npc = GameObject.Find(name);
                        if (npc == null)
                        {
                            GameObject go = Resources.Load("Prefabs\\" + category + "\\" + name, typeof(GameObject)) as GameObject;
                            npc = Instantiate(go, new Vector3(x, y, 0), Quaternion.identity);
                            npc.name = name;
                        }
                        else
                            npc.transform.position = new Vector3(x, y, 0);

                        i++;
                        break;

                    case "downstairs":
                        npc.GetComponent<Move>().Downstairs();
                        wait = true;
                        break;

                    //charaunset charaid
                    //case "charaunset")

                    //wait=true;
                    //time=para[2];
                    //with(chaid2obj[real(para[1])])instance_destroy();
                    //alarm[0]=time*room_speed;
                    //file_text_readln(file);

                    //charascale chara_name xscale yscale
                    //将名为chara_name的角色放大到(xscale, yscale)倍
                    case "charascale":
                        npc = GameObject.Find(para[1]);
                        if (npc == null)
                            Debug.Log("cannot find a GameObject called: " + para[1]);
                        else
                        {
                            //float x, y;
                            float.TryParse(para[2], out x);
                            float.TryParse(para[3], out y);
                            npc.transform.localScale = new Vector3(x, y, 1);
                        }

                        i++;
                        break;

                    //charaanime charaid index
                    //else if (para[0] == "charaanime")
                    //{
                    //chaid = real(para[1]);
                    //chaid2obj[chaid].image_index=chaidind2spr[chaid,real(para[2])];
                    //file_text_readln(file);
                    //}

                    // costume cos
                    // 牛奶酱换装cos
                    case "costume":
                        int cos;
                        int.TryParse(para[1], out cos);
                        GetComponent<PlatformerCharacter2D>().CostumeChange(cos);
                        i++;
                        break;
                    //charamove x
                    //主角走到(x, *)处
                    case "charamove":
                        GetComponent<PlatformerCharacter2D>().Move(1, false, false, false);
                        wait = true;
                        break;

                    case "vibration":
                        wait = true;
                        alarm = 2 * FPS;
                        break;

                    case "EndingFastest":
                        GameObject ef = GameObject.Find("npc_ending_fastest");
                        if (ef == null)
                            Debug.Log("can't find object: npc_ending_fastest.");
                        ef.GetComponent<EndingFastest>().EndFlagOn();
                        i++;
                        break;

                    //hp-1
                    case "hp-999":
                        GetComponent<Status>().HPChange(16);
                        i++;
                        break;

                    //add key +value
                    case "add":
                        int value;
                        //Debug.Log(commands[i]);
                        int.TryParse(para[2], out value);
                        PlayerPrefs.SetInt(para[1], PlayerPrefs.GetInt(para[1]) + value);
                        i++;
                        break;

                    //warning
                    case "warning":
                        CommandWarning();
                        break;

                    //boss
                    case "boss":
                        warning_logo = false;
                        Transform.FindObjectOfType<Boss>().GetReady();
                        i++;
                        break;

                    //plot plotno
                    //执行PLOT[plotno].txt指定的脚本
                    case "plot":
                        GetComponent<Platformer2DUserControl>().EnterAVGMode("PLOT" + para[1]);
                        can_skip = false;
                        break;

                    //gotoscene scenename
                    //进入名为scenename的scene
                    case "gotoscene":
                        act_init.GetComponent<LvInitiate>().ReloadMap(para[1]);
                        i++;
                        break;


                    //line & error
                    default:
                        pause = true;

                        speaker_name = para[0];

                        words = para[1];
                        for (int j = 2; j < para.Length; j++)
                        {
                            words += " ";
                            words += para[j];
                        }

                        //text color
                        if (speaker_name == "牛奶酱")
                            words = "<color=magenta>" + words + "</color>";
                        co_canvas.GetComponent<AVGUI>().Say(speaker_name, words);
                        break;




                        //Debug.Log("can't understand command: " + commands[i]);
                }
            }
            else
            {
                Exit();
            }
        }
        else if (wait)
        {
            switch (para[0])
            {
                case "charamove":
                    int temp;
                    int.TryParse(para[1], out temp);
                    if (transform.position.x >= temp)
                        Resume();
                    break;
                case "vibration":
                    float OriginCameraPositionY = Camera.main.transform.position.y;
                    Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, OriginCameraPositionY + 0.25f * Mathf.Sin(6 * 3.14f / FPS * (2 * FPS - alarm)), Camera.main.transform.position.z);
                    break;
            }
        }
        if (pause)
            co_canvas.SetActive(true);
        if (wait)
            co_canvas.SetActive(false);
    }

    void FixedUpdate()
    {
        if (alarm == 0)
            Resume();
        if (alarm > -1)
            alarm--;
    }

    public void Open(string binid)
    {
        commands = act_init.GetComponent<LvInitiate>().GetCutContents(binid).ToArray();
        i = 0;
    }

    public void NextPage()
    {
        if (pause)
        {
            pause = false;
            i++;
        }
    }

    public void Skip()
    {
        if (can_skip)
        {
            pause = false;
            wait = false;
            i = commands.Length - 1;
            if (commands[i].args[0] != "plot")
            {
                i += 1;
            }
        }
        // Exit();
    }

    public void Resume()
    {
        wait = false;
        i++;
    }

    private void Exit()
    {
        GetComponent<Platformer2DUserControl>().enabled = true;
        //GetComponent<hp_gauge>().enabled = true;
        GetComponent<AvgEngineInput>().enabled = false;
        co_canvas.SetActive(false);
        enabled = false;
    }

    private void CommandWarning()
    {
        wait = true;
        alarm = 60;
        warning_logo = true;
    }
}

