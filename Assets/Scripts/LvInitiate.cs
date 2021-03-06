﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate GameObject LoadTile(string tag, string name);
// used for stage loading
// a standalone ACT game scene is called a STAGE.
// STAGE
// |- LEVEL: made by TILES.
//    |- CAMERA:
//    |- PLAYER:
//    |- LEVEL ITEM: core gameplay. dealt with by physics engine.
//    |- BREAKABLE ITEM: do something when hit by player's WEAPON.
//    (check box)
//    |- ACTIVE TRIGGER: press 'up' to activate while in the check box.     <npc>
//       |- NPC:
//       |- CUSTOME GIMMICK:
//    |- PASSIVE TRIGGER: auto activate while in the check box.
//       |- EVENT ITEM: trigger to STORY.                                   <Event>
//       |- ITEM:                                                           <Item>
//    |- BACKGROUND: non-interactive.
// |- STORY: scenarios.
public class LvInitiate
{
    private bool debugMode;
    private GameObject player;
    private Story story;
    private string ThisLevel;
    // refer to the method on how to load a tile (edittime or runtime) 
    private LoadTile loadTile;

    public LvInitiate(bool pdebugMode, GameObject pplayer, LoadTile ploadTile)
    {
        debugMode = pdebugMode;
        player = pplayer;
        ThisLevel = "";
        loadTile = ploadTile;
    }

    public void Start()
    {
        if (!debugMode)
        {
            // scenename = PlayerPrefs.GetString("SceneName");
            // scenename = "0Castle1Outside";
            // Load Map
            if (ThisLevel != "")
                ReloadMap(ThisLevel);
        }
        else
        {
            ThisLevel = GameObject.Find("grid").GetComponent<Grid>().scenename;
            LoadStory();
        }

    }
    public void SetThisLevel(string pthisLevel)
    {
        ThisLevel = pthisLevel;
    }

    private void LoadMap(string MapName)
    {
        ThisLevel = MapName;

        LoadLevel();

        LoadStory();
    }
    public void ReloadMap(string MapName)
    {
        Clear();
        LoadMap(MapName);
    }

    public void Clear()
    {
        GameObject[] AllGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in AllGameObjects)
        {
            if (go.name == "grid" || go.name == "Main Camera" || go.name == "scenario" || go.name == "ACTManager")
                continue;
            // background
            if (go.name == "Background")
            {
                for (int i = 0; i < go.transform.childCount; i++)
                    if (debugMode)
                        GameObject.DestroyImmediate(go.transform.GetChild(i).gameObject);
                    else
                        GameObject.Destroy(go.transform.GetChild(i).gameObject);
                continue;
            }

            if (go.tag == "Player")
            {
                player = go;
                continue;
            }
            if (go.name == "player_hp_gauge")
                continue;
            if (go.name == "player_bullet")
                continue;
            if (debugMode)
                GameObject.DestroyImmediate(go);
            else
                GameObject.Destroy(go);
        }
    }
    public void LoadLevel(bool EditMode = false)
    {
        XmlSaver xs = new XmlSaver();
        string path = "Level/" + ThisLevel + ".lv";
        LevelInfo levelinfo = xs.GetInfo(path, typeof(LevelInfo)) as LevelInfo;

        if (levelinfo == null)
            if (debugMode)
            {
                levelinfo = new LevelInfo();
                levelinfo.Rooms = new Rect[1] { new Rect(0, 12.25f, 20, 12.25f) };
            }

        // 读入关卡配置
        Camera.main.GetComponent<CameraFollow>().CameraMode = 0;

        Camera.main.GetComponent<CameraFollow>().Rooms = new Rect[levelinfo.Rooms.Length];
        for (int i = 0; i < levelinfo.Rooms.Length; i++)
            Camera.main.GetComponent<CameraFollow>().Rooms[i] = levelinfo.Rooms[i];
        if (player != null)
            Camera.main.GetComponent<CameraFollow>().target = player.transform;

        foreach (LevelItem li in levelinfo.items)
        {
            string tag = li.tag;
            string name = li.name;
            float x = li.x;
            float y = li.y;
            string arg = "";

            LoadTile(tag, name, x, y, arg, EditMode);
        }
        // event item
        foreach (EventItem ei in levelinfo.events)
        {
            string tag = "Event";
            string name = ei.name;
            float x = ei.x;
            float y = ei.y;
            string arg = ei.arg;

            LoadTile(tag, name, x, y, arg);
        }
        foreach (Background background in levelinfo.backgrounds)
        {
            string tag = "Background";
            string name = background.name;
            float x = background.x;
            float y = background.y;
            string arg = "";

            LoadTile(tag, name, x, y, arg);
        }
    }
    private void LoadTile(string tag, string name, float x, float y, string arg, bool EditMode = false)
    {
        if (tag == "Player")
        {
            player.transform.position = new Vector3(x, y, 0);
            Camera.main.GetComponent<CameraFollow>().FindCurrentRoom();
            Camera.main.GetComponent<CameraFollow>().FollowPlayer();
        }
        else
        {
            GameObject pre;
            pre = loadTile(tag, name);

            pre.name = name;
            pre.transform.position = new Vector3(x, y, 0);

            if (tag == "Event")
                pre.GetComponent<Plot>().plotno = arg;
        }
    }
    // load story
    private void LoadStory()
    {
        XmlSaver xs = new XmlSaver();
        string path = "Text/" + ThisLevel + ".story";

        story = xs.GetInfo(path, typeof(Story)) as Story;
    }

    public List<Line> GetCutContents(string cutno)
    {
        foreach (Cut cut in story.cuts)
        {
            if (cut.no == cutno)
            {
                return cut.contents;
            }
        }
        Debug.Log("cannot load " + cutno + ".");
        return null;
    }


}
