using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Grid))]
public class GridEditor : Editor
{
    Grid grid;
    int focus;
    int[] focus_tile;
    Texture2D[][] TileTextures;
    string[][] TileNames;
    string[] catecory;

    private LvInitiate ACTManager;

    public void OnEnable()
    {
        grid = (Grid)target;

        GameObject player = GameObject.Find("milk");
        if (!player)
            Debug.Log("cannot find milk.");
        ACTManager = new LvInitiate(true, player, LoadTileOnEdit);

        // toolbar init
        focus = 0;
        catecory = new string[] { "Wall", "Enemy", "ActiveTrigger", "Event" };
        int l_catecory = catecory.Length;
        focus_tile = new int[l_catecory];
        for (int i = 0; i < l_catecory; i++)
            focus_tile[i] = 0;
        TileTextures = new Texture2D[l_catecory][];
        TileNames = new string[l_catecory][];

        // load tiles
        for (int i = 0; i < l_catecory; i++)
        {
            Object[] WallTiles = Resources.LoadAll("Prefabs\\" + catecory[i]);// as GameObject[];
            int l_WallTiles = WallTiles.Length;
            TileTextures[i] = new Texture2D[l_WallTiles];
            TileNames[i] = new string[l_WallTiles];
            int j = 0;
            foreach (Object tile in WallTiles)
            {
                GameObject gotile = tile as GameObject;
                SpriteRenderer sr = gotile.GetComponent<SpriteRenderer>();
                if (sr != null)
                    TileTextures[i][j] = gotile.GetComponent<SpriteRenderer>().sprite.texture;
                else
                    TileTextures[i][j] = null;
                TileNames[i][j] = gotile.name;
                j++;
            }
        }

        SceneView.onSceneGUIDelegate = GridUpdate;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void GridUpdate(SceneView sceneview)
    {
        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(0, 0, 1000, 640));
        // GUILayout.BeginHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Scene Name: ", GUILayout.Width(128));
        grid.scenename = GUILayout.TextField(grid.scenename, GUILayout.Width(128));
        GUILayout.EndHorizontal();
        focus = GUILayout.Toolbar(focus, catecory, GUILayout.Width(64 * catecory.Length));

        focus_tile[focus] = GUILayout.Toolbar(focus_tile[focus], TileTextures[focus], GUILayout.Width(64 * TileTextures[focus].Length), GUILayout.Height(64));
        // GUILayout.EndHorizontal();
        GUILayout.Label("press a to add, s to save, d to load,\nwhen the scene window is activated.");

        GUILayout.EndArea();
        Handles.EndGUI();

        Event e = Event.current;

        Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
        Vector3 mousePos = r.origin;

        if (e.isKey)
        {
            switch (e.character)
            {
                case 'a':
                    string name = TileNames[focus][focus_tile[focus]];
                    Object loaded = Resources.Load("Prefabs\\" + catecory[focus] + "\\" + name, typeof(GameObject));
                    GameObject pre = PrefabUtility.InstantiatePrefab(loaded) as GameObject;
                    pre.name = name;
                    pre.tag = catecory[focus];

                    Vector3 aligned = new Vector3(Mathf.Floor(mousePos.x / grid.width) * grid.width + grid.width / 2, Mathf.Floor(mousePos.y / grid.height) * grid.height + grid.height / 2, 0);
                    pre.transform.position = aligned;

                    break;
                //save the map
                case 's':
                    Debug.Log("save map start!");

                    string path = "Level";

                    //check the directory
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    path += "/" + grid.scenename + ".lv";

                    LevelInfo levelinfo = new LevelInfo();

                    GameObject[] AllGameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
                    foreach (GameObject go in AllGameObjects)
                    {
                        if (go.name == "grid" || go.name == "scenario" || go.name == "ACTManager")
                            continue;
                        if (go.name == "Main Camera")
                        {
                            levelinfo.Rooms = go.GetComponent<CameraFollow>().Rooms;
                            continue;
                        }
                        if (go.transform.parent != null)
                            continue;
                        if (go.tag == "Event")
                        {
                            EventItem ei = new EventItem();
                            ei.name = go.name;
                            ei.x = go.transform.position.x;
                            ei.y = go.transform.position.y;
                            if (go.name == "GotoPlot")
                                ei.arg = go.GetComponent<Plot>().plotno;
                            else if (go.name == "GotoScene")
                                ei.arg = go.GetComponent<GotoScene>().scenename;
                            else if (go.name == "GotoMap")
                                ei.arg = go.GetComponent<Plot>().plotno;
                            levelinfo.events.Add(ei);
                        }
                        else if (go.name == "Background")
                        {
                            SpriteRenderer[] spriteRenderers = go.GetComponentsInChildren<SpriteRenderer>();
                            foreach (SpriteRenderer spriteRenderer in spriteRenderers)
                            {
                                Background background = new Background();

                                background.name = spriteRenderer.name;
                                background.x = spriteRenderer.transform.position.x;
                                background.y = spriteRenderer.transform.position.y;

                                levelinfo.backgrounds.Add(background);
                            }
                        }
                        else
                        {
                            LevelItem li = new LevelItem();
                            li.tag = go.tag;
                            li.name = go.name;
                            li.x = go.transform.position.x;
                            li.y = go.transform.position.y;
                            levelinfo.items.Add(li);
                        }
                    }

                    XmlSaver xs = new XmlSaver();
                    string datastring = xs.SerializeObject(levelinfo, typeof(LevelInfo));
                    xs.CreateXML(path, datastring);

                    Debug.Log("save map success!");
                    break;
                // load the map
                case 'd':
                    ACTManager.Clear();
                    ACTManager.SetThisLevel(grid.scenename);
                    ACTManager.LoadLevel(true);
                    break;
            }
        }
    }
    public GameObject LoadTileOnEdit(string tag, string name)
    {
        Object loaded = Resources.Load("Prefabs\\" + tag + "\\" + name, typeof(GameObject));
        if (!loaded)
            Debug.Log("tile " + name + " load failed.");

        return PrefabUtility.InstantiatePrefab(loaded) as GameObject;
    }
}
