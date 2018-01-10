using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvInitiate : MonoBehaviour
{
    private string scenename;
    private GameObject player = null;
    private Story story;
    // Use this for initialization
    void OnEnable()
    {
        scenename = PlayerPrefs.GetString("SceneName");
        scenename = "0Castle0Party";
        // scenename = "0Castle1Outside";
        // Load Map
        LoadMap(scenename);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LoadMap(string MapName)
    {
        XmlSaver xs = new XmlSaver();
        string path = "Level/" + MapName + ".lv";
        LevelInfo levelinfo = xs.GetInfo(path, typeof(LevelInfo)) as LevelInfo;

        Camera.main.GetComponent<CameraFollow>().CameraMode = 0;

        Camera.main.GetComponent<CameraFollow>().Rooms = new Rect[levelinfo.Rooms.Length];
        Camera.main.GetComponent<CameraFollow>().Rooms = levelinfo.Rooms;

        foreach (LevelItem li in levelinfo.items)
        {
            string tag = li.tag;
            string name = li.name;
            float x = li.x;
            float y = li.y;

            if (li.tag != "Player" || (li.tag == "Player" && player == null))
            {
                GameObject pre = Resources.Load("Prefabs\\" + tag + "\\" + name, typeof(GameObject)) as GameObject;
                if (!pre)
                    Debug.Log("tile " + name + " load failed.");

                pre = Instantiate(pre, new Vector3(x, y, 0), Quaternion.identity);
                pre.name = name;
            }
            else
            {
                player.transform.position = new Vector3(x, y, 0);
                Camera.main.GetComponent<CameraFollow>().FindCurrentRoom();
                Camera.main.GetComponent<CameraFollow>().FollowPlayer();
            }
        }
        foreach (EventItem ei in levelinfo.events)
        {
            string tag = "Event";
            string name = ei.name;
            float x = ei.x;
            float y = ei.y;

            GameObject pre = Resources.Load("Prefabs\\" + tag + "\\" + name, typeof(GameObject)) as GameObject;
            if (!pre)
                Debug.Log("tile " + name + " load failed.");

            pre = Instantiate(pre, new Vector3(x, y, 0), Quaternion.identity);
            pre.name = name;
            if (name == "GotoPlot")
                pre.GetComponent<Plot>().plotno = ei.arg;
            else if (name == "GotoScene")
                pre.GetComponent<GotoScene>().scenename = ei.arg;
        }

        // read story
        path = "Text/" + MapName + ".story";
        if (xs.hasFile(path))
        {
            string datastring = xs.LoadXML(path);
            story = xs.DeserializeObject(datastring, typeof(Story)) as Story;
        }
    }
    public void ReloadMap(string MapName)
    {
        GameObject[] AllGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject go in AllGameObjects)
        {
            if (go == gameObject)
                continue;
            if (go.name == "grid" || go.name == "Main Camera" || go.name == "scenario")
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
            GameObject.Destroy(go);
        }
        LoadMap(MapName);
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
