using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//GameData,储存数据的类，把需要储存的数据定义在GameData之内就行//
public class GameData
{
    //密钥,用于防止拷贝存档//
    public string key;

    //下面是添加需要储存的内容//
    public int placeProgress;
    public int crystal;
    public string plot;
    public int fairy;

    public GameData()
    {
        placeProgress = -1;
        crystal = 0;
        plot = "000";
        fairy = 0;
    }
}
//管理数据储存的类//
public class GameDataManager
{
    private string dataFileName = "save";//存档文件的名称,自己定//
    private XmlSaver xs = new XmlSaver();

    public GameData gameData;

    public GameDataManager()
    {
        gameData = new GameData();
    }

    //设定密钥，根据具体平台设定//
    // gameData.key = SystemInfo.deviceUniqueIdentifier;

    //存档时调用的函数//
    public void Save(int current)
    {
        string gameDataFile = GetDataPath() + "/" + dataFileName + current.ToString() + ".sav";

        string dataString = xs.SerializeObject(gameData, typeof(GameData));
        xs.CreateXML(gameDataFile, dataString);
    }

    //读档时调用的函数//
    public void Load(int current)
    {
        string gameDataFile = GetDataPath() + "/" + dataFileName + current.ToString() + ".sav";
        if (xs.hasFile(gameDataFile))
        {
            string dataString = xs.LoadXML(gameDataFile);
            GameData gameDataFromXML = xs.DeserializeObject(dataString, typeof(GameData)) as GameData;

            //是合法存档//
            // if (gameDataFromXML.key == gameData.key)
            {
                gameData = gameDataFromXML;

                LoadGameData(gameData);

            }
            //是非法拷贝存档//
            // else
            {
                // Debug.Log("save file broken");
                //留空：游戏启动后数据清零，存档后作弊档被自动覆盖//
            }
        }

    }
    public void LoadGameData(GameData gameData)
    {
        PlayerPrefs.SetInt("placeProgress", gameData.placeProgress);
        PlayerPrefs.SetInt("Crystal", gameData.crystal);
        PlayerPrefs.SetString("Plot", gameData.plot);
        PlayerPrefs.SetInt("Fairy", gameData.fairy);

        // load from title
        if (SceneManager.GetActiveScene().name == "Title")
            SceneManager.LoadScene("ACT");
    }
    public string Check(int current)
    {
        string gameDataFile = GetDataPath() + "/" + dataFileName + current.ToString() + ".sav";
        if (xs.hasFile(gameDataFile))
        {
            string dataString = xs.LoadXML(gameDataFile);
            GameData gameDataFromXML = xs.DeserializeObject(dataString, typeof(GameData)) as GameData;

            //是合法存档//
            // if (gameDataFromXML.key == gameData.key)
            {
                gameData = gameDataFromXML;
                return gameData.placeProgress.ToString();
            }
            //是非法拷贝存档//
            // else
            {
                // go_datainfo[current].GetComponent<Text>().text = "BAD DATA";
                //留空：游戏启动后数据清零，存档后作弊档被自动覆盖//
            }
        }
        else
            return "NO DATA";

    }

    //获取路径//
    private static string GetDataPath()
    {
        //return Application.dataPath;
        return "Save";
    }
}