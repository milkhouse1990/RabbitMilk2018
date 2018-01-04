using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeAvgEngine : MonoBehaviour {

    public Texture2D icon;
    public Texture2D icon_milk;
    public Texture2D frame;
    public Texture2D frame1;
    public Texture2D np;

    /*
    0 nextscene
    1 hime
    */
    public GameObject[] createid;
    private GameObject npc;

    private string[] text;
    private string dialogue_next = "";
    //ifstream file;
    private string[] commands;
    private int i = 0;
    private int speaker = -1;
    private string speaker_name = "";
    /*
    1 milk
    2 cola
    3 hime
    */
    private string[] speakerid = { "0", "ミルクちゃん", "可乐炭", "公主殿下" };
    private string words = "";
    private bool pause = false;
    private bool wait = false;
    private int alarm=-1;

    private const int FPS = 60;
    private const int BYTEPERLINE = 20;

    //for debug
    private string errmsg = "";
    private bool err = false;

    //for button a
    private int guialpha = 20;
    private bool al = true;
    public GUIStyle gs;
    // Use this for initialization
    void Start()
    {
        System.Text.Encoding.GetEncoding("gb2312");
    }

    // Update is called once per frame
    void Update()
    {
        guialpha--;
        if (guialpha == 0)
        {
            guialpha = 20;
            al = !al;
        }
        /*if keyboard_check_pressed(global.my_START)
        {
        file_text_close(file);
        file = -1;
        avgend = true;
        }

        if pause
        {
        if keyboard_check_pressed(global.my_A)
        {
        file_text_readln(file);
        pause = false;
        err = false;
        }
        }
        else if wait
        {
        }
        else*/
        if (!pause && !wait)
        {
            //command load
            if (i < commands.Length)
            {
                //command analysis
                //int i = 0;
                string[] para = commands[i].Split(' ');
                //command understanding
                switch (para[0])
                {
                    //pause time
                    case "pause":
                        wait = true;
                        int temp;
                        int.TryParse(para[1], out temp);
                        alarm = temp * FPS;
                        break;
                    //say charaid text
                    case "say":

                        pause = true;
                        alarm = 1 * FPS;
                        int.TryParse(para[1], out speaker);
                        speaker_name = speakerid[speaker];
                        words = para[2];
                        for (int j = 3; j < para.Length; j++)
                        {
                            words += " ";
                            words += para[j];
                        }
                        if (speaker == 1)
                            words = "<color=magenta>" + words + "</color>";
                        break;

                    //say0 name text
                    case "say0":

                        wait = true;
                        speaker = 0;
                        speaker_name = para[1];
                        words = para[2];
                        for (int j = 3; j < para.Length; j++)
                        {
                            words += " ";
                            words += para[j];
                        }
                        break;

                    //create id x y
                    case "create":
                        int id, x, y;
                        int.TryParse(para[1], out id);
                        int.TryParse(para[2], out x);
                        int.TryParse(para[3], out y);
                        npc = Instantiate(createid[id], new Vector3(x, y, 0), Quaternion.identity);
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

                    //charascale charaid xscale yscale
                    //else if (para[0] == "charascale")
                    //{
                    //charaid = real(para[1]);
                    //charax = real(para[2]);
                    //charay = real(para[3]);
                    //chaid2obj[charaid].image_xscale = charax;
                    //chaid2obj[charaid].image_yscale = charay;

                    //file_text_readln(file);
                    //}
                    //charaanime charaid index
                    //else if (para[0] == "charaanime")
                    //{
                    //chaid = real(para[1]);
                    //chaid2obj[chaid].image_index=chaidind2spr[chaid,real(para[2])];
                    //file_text_readln(file);
                    //}
                    //charamove charaid vx vy
                    case "charamove":

                        GetComponent<PlatformerCharacter2D>().Move(1, false, false, false);
                        //wait = true;
                        //i--;
                        //chaid = real(para[1]);
                        //if instance_exists(chaid2obj[chaid])
                        //{
                        //	chaid2obj[chaid].hspeed = real(para[2]);
                        //	chaid2obj[chaid].vspeed = real(para[3]);
                        //	file_text_readln(file);
                        //}
                        //else
                        //{
                        //	err = true;
                        //	errmsg = "can't find object " + para[1];
                        //	pause = true;
                        //}
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
                    //error
                    default:

                        errmsg = "can't understand command: " + para[0];
                        err = true;
                        wait = true;
                        break;
                }
            }
            else
            {
                GetComponent<Platformer2DUserControl>().enabled = true;
                //GetComponent<hp_gauge>().enabled = true;
                GetComponent<AvgEngineInput>().enabled = false;
                enabled = false;
            }
        }
    }
    void FixedUpdate()
    {
        if (alarm==0)
        {
            pause = false;
            i++;
        }
        if (alarm > -1)
            alarm--;
    }


    void OnGUI()
    {
        const int xscreen = 1280, yscreen = 720, tile = 64;
        if (err)
        {
            GUI.Label(new Rect(100, 0, 640, 320), errmsg);
        }
        else if (pause)
        {

            /*
            //milk
            if (speaker = "1")
            {
            //cos
            draw_sprite(spr_avatar_milk, global.necklace, view_xview + 5, view_yview + 5)
            //face
            if health > 8
            {
            draw_sprite(spr_avatar_milkf, 0, view_xview + 5, view_yview + 5);
            }
            else
            {
            draw_sprite(spr_avatar_milkf, 2, view_xview + 5, view_yview + 5);
            }
            image_speed = 0;
            }
            //chara

            else
            {
            draw_sprite(spr_avatar, real(speaker) - 2, view_xview + 635, view_yview + 5)
            }

            //dialogue
            draw_sprite(spr_dialogue, 0, view_xview + pos, view_yview + 5)
            draw_sprite_ext(spr_nextpage, 0, view_xview + 450 + pos, view_yview + 70, 1, 1, 1, c_white, al)

            */


            //TALK
            //draw_text(0, 0, speaker);
            //int i = 0;

            /*if (words != "")
            {

            words = "";
            }*/
            if (words != "")
            {
                //icon
                if (speaker == 1)
                    GUI.DrawTexture(new Rect(0, 720 - 196, 128, 196), icon_milk);
                //frame
                if (speaker == 1)
                    GUI.Label(new Rect(2 * tile, yscreen - 2 * tile, xscreen - 4 * tile, 2 * tile), frame1);
                else
                    GUI.Label(new Rect(2 * tile, yscreen - 2 * tile, xscreen - 4 * tile, 2 * tile), frame);
                //nextpage
                if (al)
                    GUI.Label(new Rect(xscreen - 4f * tile, yscreen - tile, tile, tile), np);
                //name
                GUI.Label(new Rect(3f * tile, yscreen - 2.5f * tile, xscreen - 6 * tile, 2 * tile), speaker_name, gs);
                //words           

                GUI.Label(new Rect(3f * tile, yscreen - 1.5f * tile, xscreen - 6 * tile, 2 * tile), words, gs);
            }

        }

        //else if para[0] == "charaset"
        {
        }

    }

    public void Open(string[] plot)
    {
        commands = plot;
        if (commands == null)
            Debug.Log("plot load failed.");
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

    public void Resume()
    {
        wait = false;
        i++;
    }
}
