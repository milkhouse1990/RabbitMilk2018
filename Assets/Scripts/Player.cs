using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private ModeSwitch modeSwitch;
    void Awake()
    {
        modeSwitch = GameObject.Find("ACTManager").GetComponent<ModeSwitch>();
    }
    // Use this for initialization
    void Start()
    {

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
                            if (Input.GetButtonDown("up"))
                            {
                                arg = checkBox.GetComponent<Npc>().npcno;
                                if (arg != "")
                                {
                                    string binid = "NPC" + arg;
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
                            break;
                    }
                    if (triggerOn)
                        break;
                }
            }
        }
    }
}
