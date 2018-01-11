using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LvInitiate : MonoBehaviour
{
    public bool DebugMode = false;
    private string scenename;
    private GameObject player = null;
    private Story story;
    public string ThisLevel;
    public string LastLevel;
    // Use this for initialization
    void OnEnable()
    {
        if (!DebugMode)
        {
            scenename = PlayerPrefs.GetString("SceneName");
            scenename = "0Castle0Party";
            // scenename = "0Castle1Outside";
            ThisLevel = scenename;
            // Load Map
            LoadMap(scenename);
        }
        else
        {
            ThisLevel = GameObject.Find("grid").GetComponent<Grid>().scenename;
            LoadStory();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LoadMap(string MapName)
    {
        LastLevel = ThisLevel;
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
            if (go.name == "grid" || go.name == "Main Camera" || go.name == "scenario" || go.name == "ACTInit")
                continue;
            if (go.transform.parent != null)
                continue;
            if (go.tag == "Player")
            {
                player = go;
                continue;
            }
            if (go.name == "AVGCanvas" || go.name == "player_hp_gauge")
                continue;
            if (go.name == "player_bullet")
                continue;
            if (DebugMode)
                GameObject.DestroyImmediate(go);
            else
                GameObject.Destroy(go);
        }
    }
    public void LoadLevel()
    {
        XmlSaver xs = new XmlSaver();
        string path = "Level/" + ThisLevel + ".lv";
        LevelInfo levelinfo = xs.GetInfo(path, typeof(LevelInfo)) as LevelInfo;

        if (levelinfo == null)
            if (DebugMode)
            {
                levelinfo = new LevelInfo();
                levelinfo.Rooms = new Rect[1] { new Rect(0, 12.25f, 20, 12.25f) };
            }

        Camera.main.GetComponent<CameraFollow>().CameraMode = 0;

        Camera.main.GetComponent<CameraFollow>().Rooms = new Rect[levelinfo.Rooms.Length];
        Camera.main.GetComponent<CameraFollow>().Rooms = levelinfo.Rooms;

        foreach (LevelItem li in levelinfo.items)
        {
            string tag = li.tag;
            string name = li.name;
            float x = li.x;
            float y = li.y;
            string arg = "";

            LoadTile(tag, name, x, y, arg);
        }
        foreach (EventItem ei in levelinfo.events)
        {
            string tag = "Event";
            string name = ei.name;
            float x = ei.x;
            float y = ei.y;
            string arg = ei.arg;

            LoadTile(tag, name, x, y, arg);
        }
    }
    private void LoadTile(string tag, string name, float x, float y, string arg)
    {
        if (tag != "Player" || (tag == "Player" && player == null))
        {
            GameObject pre;
            if (DebugMode)
            {
                Object loaded = Resources.Load("Prefabs\\" + tag + "\\" + name, typeof(GameObject));
                if (!loaded)
                    Debug.Log("tile " + name + " load failed.");

                pre = PrefabUtility.InstantiatePrefab(loaded) as GameObject;
            }
            else
            {
                pre = Resources.Load("Prefabs\\" + tag + "\\" + name, typeof(GameObject)) as GameObject;
                if (!pre)
                    Debug.Log("tile " + name + " load failed.");
                pre = Instantiate(pre);
            }
            pre.name = name;
            if (name == "GotoPlot")
                pre.GetComponent<Plot>().plotno = arg;
            else if (name == "GotoScene")
                pre.GetComponent<GotoScene>().scenename = arg;

            pre.transform.position = new Vector3(x, y, 0);
        }
        else
        {
            player.transform.position = new Vector3(x, y, 0);
            Camera.main.GetComponent<CameraFollow>().FindCurrentRoom();
            Camera.main.GetComponent<CameraFollow>().FollowPlayer();
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
        Debug.Log("cannot load " + cutno + " .");
        return null;
    }
}
