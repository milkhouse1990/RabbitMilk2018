using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private ModeSwitch modeSwitch;

    // for npc
    private CheckBox npc;
    // a sign telling you to press UP.
    GameObject up;
    void Awake()
    {
        GameObject actmanager = GameObject.Find("ACTManager");
        modeSwitch = actmanager.GetComponent<ModeSwitch>();
        up = actmanager.transform.Find("ACTCanvas").Find("Up").gameObject;
        // up.SetActive(false);

        actmanager.transform.Find("ACTCanvas").Find("BossHpGauge").gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {

    }
    void OnEnable()
    {
        up.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (modeSwitch.currentMode == "act")
        {
            CheckBox[] checkBoxes = FindObjectsOfType<CheckBox>() as CheckBox[];
            foreach (CheckBox checkBox in checkBoxes)
            {
                if (checkBox.CheckIn(GetComponent<ColliderBox>()))
                {
                    bool triggerOn = false;
                    switch (checkBox.tag)
                    {
                        case "Event":
                            string arg = checkBox.GetComponent<Plot>().plotno;
                            switch (checkBox.name)
                            {
                                case "GotoPlot":
                                    string binid = "PLOT" + arg;
                                    modeSwitch.EnterMode("avg", binid);
                                    GameObject.Destroy(checkBox.gameObject);
                                    break;
                                case "GotoScene":
                                    SceneManager.LoadScene(arg);
                                    break;
                                case "GotoMap":
                                    modeSwitch.EnterMode("map", arg);
                                    break;
                            }
                            triggerOn = true;
                            break;
                        case "Item":
                            switch (checkBox.name)
                            {
                                case "item_crystal":
                                    int crystal = PlayerPrefs.GetInt("Crystal", 0) + 1;
                                    if (crystal > 999)
                                        crystal = 999;
                                    PlayerPrefs.SetInt("Crystal", crystal);
                                    break;
                                case "item_heart":
                                    GetComponent<Status>().HPChange(-1);
                                    break;
                                case "Chip":
                                    SystemDataManager systemDataManager = new SystemDataManager();
                                    systemDataManager.Load();

                                    arg = checkBox.GetComponent<Arg>().arg;
                                    string no = arg.Substring(1);
                                    int temp;
                                    int.TryParse(no, out temp);
                                    switch (arg[0])
                                    {
                                        case 'B':
                                            systemDataManager.systemdata.setting[temp] = true;
                                            break;
                                    }

                                    systemDataManager.Save();
                                    break;
                            }
                            GameObject.Destroy(checkBox.gameObject);
                            triggerOn = true;
                            break;
                        case "npc":
                            if (!npc)
                            {
                                // meet a npc
                                npc = checkBox;
                                up.SetActive(true);
                            }
                            if (Input.GetButtonDown("up"))
                            {
                                Npc conpc = checkBox.GetComponent<Npc>();
                                if (conpc != null)
                                {
                                    string binid = "NPC" + conpc.npcno;
                                    modeSwitch.EnterMode("avg", binid);
                                }
                                else
                                    switch (checkBox.name)
                                    {
                                        case "tree":
                                            checkBox.GetComponent<InsideOutsideTree>().EnterTree();
                                            break;
                                    }
                                triggerOn = true;
                            }
                            else
                            {
                                // with the npc
                                up.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
                            }
                            break;
                    }
                    if (triggerOn)
                        break;
                }
                else
                {
                    if (checkBox == npc || npc == null)
                    {
                        // leave the npc
                        npc = null;
                        up.SetActive(false);
                    }
                }
            }
        }
    }
}
