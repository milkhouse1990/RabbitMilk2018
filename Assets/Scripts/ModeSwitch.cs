using UnityEngine;
using UnityEngine.SceneManagement;

// control mode activation and switch
public class ModeSwitch : MonoBehaviour
{
    public GameObject player;

    private string currentMode;
    // UI
    private GameObject mapCanvas;
    private GameObject avgCanvas;
    private GameObject fairyCanvas;

    void Awake()
    {
        mapCanvas = transform.Find("MapCanvas").gameObject;
        avgCanvas = transform.Find("AVGCanvas").gameObject;
        fairyCanvas = transform.Find("FairyCanvas").gameObject;
    }
    void Update()
    {
        switch (currentMode)
        {
            case "act":

                // check npc
                if (Input.GetButtonDown("up"))
                {
                    Npc[] npcs = FindObjectsOfType<Npc>() as Npc[];
                    foreach (Npc npc in npcs)
                    {
                        if (npc.CheckIn(player.GetComponent<ColliderBox>()))
                        {
                            string binid = "NPC" + npc.npcno;
                            avgCanvas.GetComponent<AvgEngine>().Open(binid);
                            EnterMode("avg");
                            break;
                        }
                    }
                }
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
    void FixedUpdate()
    {
        if (currentMode == "act")
        {

            // check plot
            Plot[] plots = FindObjectsOfType<Plot>() as Plot[];
            foreach (Plot plot in plots)
            {
                if (player.transform.position.x > plot.transform.position.x)
                {
                    if (plot.name == "GotoPlot")
                    {
                        string binid = "PLOT" + plot.plotno;
                        avgCanvas.GetComponent<AvgEngine>().Open(binid);
                        EnterMode("avg");
                        GameObject.Destroy(plot.gameObject);
                        break;
                    }
                    else if (plot.name == "GotoScene")
                        SceneManager.LoadScene(plot.plotno);
                    else if (plot.name == "GotoMap")
                    {
                        EnterMode("map");
                        int temp;
                        int.TryParse(plot.plotno, out temp);
                        mapCanvas.GetComponent<MapMove>().AddPlace(temp);
                    }
                }
            }
        }
    }
    public void EnterMode(string mode)
    {
        mapCanvas.SetActive(false);

        player.GetComponent<Platformer2DUserControl>().enabled = false;

        avgCanvas.SetActive(false);

        fairyCanvas.SetActive(false);

        currentMode = mode;
        switch (mode)
        {
            case "map":
                mapCanvas.SetActive(true);
                break;
            case "act":
                player.GetComponent<Platformer2DUserControl>().enabled = true;
                break;
            case "avg":
                avgCanvas.SetActive(true);
                player.GetComponent<Physics2DM>().velocity = new Vector2(0, 0);
                player.GetComponent<PlatformerCharacter2D>().Move(0, false, false, true);
                break;
            case "fairy":
                fairyCanvas.SetActive(true);
                break;
        }

    }
}
