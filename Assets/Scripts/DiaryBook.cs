using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DiaryBook : MonoBehaviour
{
    private int current = 0;
    private Transform[] cursors;
    private Text[] datainfo;
    private Text saveload;
    private bool saveFlag;
    private GameDataManager gameDataManager;

    void Awake()
    {
        cursors = new Transform[2];
        for (int i = 0; i < 2; i++)
            cursors[i] = transform.Find("cursor" + i.ToString());

        datainfo = new Text[3];
        for (int i = 0; i < 3; i++)
        {
            datainfo[i] = transform.Find("datainfo" + i.ToString()).GetComponent<Text>();
        }

        saveload = transform.Find("saveload").GetComponent<Text>();
        gameDataManager = new GameDataManager();
    }
    // Use this for initialization
    void Start()
    {
        CursorUpdate();
    }

    void OnEnable()
    {
        for (int i = 0; i < 3; i++)
            datainfo[i].text = gameDataManager.Check(i);
    }

    // Update is called once per frame
    void Update()
    {
        // list
        int dataCompacity = 3;
        if (Input.GetButtonDown("up"))
        {
            current--;
            if (current < 0)
                current = dataCompacity - 1;
            CursorUpdate();
        }
        if (Input.GetButtonDown("down"))
        {
            current++;
            if (current == dataCompacity)
                current = 0;
            CursorUpdate();
        }

        if (Input.GetButtonDown("A"))
        {
            if (saveFlag)
            {
                // make game data
                GameData gameData = gameDataManager.gameData;
                gameData.placeProgress = GetComponentInParent<MapMove>().GetPlaceProgress();
                gameData.crystal = PlayerPrefs.GetInt("Crystal");
                gameData.plot = PlayerPrefs.GetString("Plot");
                gameData.fairy = PlayerPrefs.GetInt("Fairy");

                gameDataManager.Save(current);
                gameDataManager.Check(current);
            }
            else
            {
                gameDataManager.Load(current);
                // load from mapcanvas
                if (SceneManager.GetActiveScene().name != "Title")
                    transform.parent.parent.GetComponent<ModeSwitch>().LoadSaveFile();

                gameObject.SetActive(false);
            }
        }
    }
    private void CursorUpdate()
    {
        for (int i = 0; i < 2; i++)
        {
            cursors[i].position = new Vector2(-270 + 640, 220 + 360) + new Vector2(710 * i, -220 * current);
        }
    }
    public void SetSaveFlag(bool pSaveFlag)
    {
        saveFlag = pSaveFlag;
        saveload.text = saveFlag ? "SAVE" : "LOAD";
    }
    public bool GetSaveFlag()
    {
        return saveFlag;
    }
}
