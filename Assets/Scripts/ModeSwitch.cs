using UnityEngine;

// control mode activation and switch
// toppest manager of the ACT game scene
public class ModeSwitch : MonoBehaviour
{
    public bool debugMode = false;
    public LvInitiate lvInitiate;
    public GameObject player;

    public string currentMode;
    // UI
    private GameObject actCanvas;
    private GameObject mapCanvas;
    private GameObject avgCanvas;
    private GameObject fairyCanvas;

    void Awake()
    {
        actCanvas = transform.Find("ACTCanvas").gameObject;
        mapCanvas = transform.Find("MapCanvas").gameObject;
        avgCanvas = transform.Find("AVGCanvas").gameObject;
        fairyCanvas = transform.Find("FairyCanvas").gameObject;

        player = GameObject.Find("milk");
        if (!player)
            Debug.Log("cannot find milk.");
        GetComponentInChildren<AvgEngine>().player = player;

        lvInitiate = new LvInitiate(debugMode, player, LoadTileOnRun);
    }
    void Start()
    {
        Canvas[] canvases = GetComponentsInChildren<Canvas>();
        foreach (Canvas canvas in canvases)
            canvas.enabled = true;

        LoadSaveFile();

        lvInitiate.Start();
    }
    void Update()
    {
        switch (currentMode)
        {
            case "act":
                // activate map canvas
                if (Input.GetButtonDown("SELECT"))
                {
                    EnterMode("map");
                }
                // activate fairy canvas
                if (Input.GetButtonDown("START"))
                    EnterMode("fairy");
                break;
            case "fairy":
                if (Input.GetButtonDown("START"))
                {
                    // fairy system get effect
                    FairySystem fairy = fairyCanvas.transform.Find("FairySystem").GetComponent<FairySystem>();
                    if (fairy.GetEquip() == 0)
                    {
                        player.GetComponent<Status>().SetHPMax(16 + fairy.GetLvA(0));
                    }
                    else
                        player.GetComponent<Status>().SetHPMax(16);
                    if (fairy.GetEquip() == 1)
                    {
                        PlayerPrefs.SetInt("DropHeart", 5 * fairy.GetLvA(1));
                        PlayerPrefs.SetInt("DropCrystal", 5 * fairy.GetLvB(1));
                    }
                    else
                    {
                        PlayerPrefs.SetInt("DropHeart", 0);
                        PlayerPrefs.SetInt("DropCrystal", 0);
                    }

                    EnterMode("act");
                }
                break;
        }
    }

    public void LoadSaveFile()
    {
        if (!debugMode)
        {
            int placeProgress = PlayerPrefs.GetInt("placeProgress");
            if (placeProgress == -1)
                lvInitiate.SetThisLevel("0Castle0Party");
            else
            {
                mapCanvas.GetComponent<MapMove>().PlacesUpdate(placeProgress);
                EnterMode("map");
                return;
            }
        }
        EnterMode("act");
    }
    public void EnterMode(string mode, string arg = "")
    {
        mapCanvas.SetActive(false);

        actCanvas.SetActive(false);
        player.GetComponent<Platformer2DUserControl>().enabled = false;

        avgCanvas.SetActive(false);

        fairyCanvas.SetActive(false);

        currentMode = mode;
        switch (mode)
        {
            case "map":
                player.GetComponent<Physics2DM>().velocity = new Vector2(0, 0);
                mapCanvas.SetActive(true);
                if (arg != "")
                {
                    int temp;
                    int.TryParse(arg, out temp);
                    mapCanvas.GetComponent<MapMove>().PlacesUpdate(temp);
                }
                break;
            case "act":
                actCanvas.SetActive(true);
                player.GetComponent<Platformer2DUserControl>().enabled = true;
                player.GetComponent<Physics2DM>().enabled = true;
                break;
            case "avg":
                avgCanvas.SetActive(true);
                avgCanvas.GetComponent<AvgEngine>().Open(arg);
                player.GetComponent<Physics2DM>().velocity = new Vector2(0, 0);
                player.GetComponent<PlatformerCharacter2D>().Move(0, false, false, true);
                break;
            case "fairy":
                fairyCanvas.SetActive(true);
                break;
        }

    }
    private GameObject LoadTileOnRun(string tag, string name)
    {
        GameObject pre = Resources.Load("Prefabs\\" + tag + "\\" + name, typeof(GameObject)) as GameObject;
        if (!pre)
            Debug.Log("tile " + name + " load failed.");
        if (tag == "Background")
            pre = Object.Instantiate(pre, GameObject.Find("Background").transform);
        else
            pre = Object.Instantiate(pre);
        return pre;
    }
}
