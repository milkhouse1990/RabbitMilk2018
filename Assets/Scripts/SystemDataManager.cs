using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//SystemData 系统存档类
public class SystemData
{
    //密钥,用于防止拷贝存档//
    //public string key;

    //下面是添加需要储存的内容//
    public bool[] character;
    public bool[] setting;
    public bool[] ending;
    public SystemData()
    {
        character = new bool[7];
        int i;
        bool init_dis = false;
        for (i = 0; i < character.Length; i++)
            character[i] = init_dis;
        setting = new bool[5];
        for (i = 0; i < setting.Length; i++)
            setting[i] = init_dis;
        ending = new bool[3];
        for (i = 0; i < ending.Length; i++)
            ending[i] = init_dis;
    }
}

public class SystemDataManager
{

    private XmlSaver xs = new XmlSaver();
    public SystemData systemdata;

    public void Create()
    {
        string systemDataFile = "Save";
        if (!Directory.Exists(systemDataFile))
            Directory.CreateDirectory(systemDataFile);
        systemDataFile += "/systemsave.sav";
        systemdata = new SystemData();

        string dataString = xs.SerializeObject(systemdata, typeof(SystemData));
        xs.CreateXML(systemDataFile, dataString);
    }
    public void Load()
    {
        string gameDataFile = "Save/systemsave.sav";
        if (!xs.hasFile(gameDataFile))
            Create();
        {
            string dataString = xs.LoadXML(gameDataFile);
            SystemData gameDataFromXML = xs.DeserializeObject(dataString, typeof(SystemData)) as SystemData;

            systemdata = gameDataFromXML;
            //Debug.Log(systemdata.character[0]);
        }

    }
    public bool[] LoadBools(string key)
    {
        Load();
        switch (key)
        {
            case "character_collection":
                return systemdata.character;
            case "setting_collection":
                return systemdata.setting;
            case "ending_collection":
                return systemdata.ending;
            default:
                bool[] wrong_data = new bool[1] { false };
                return wrong_data;
        }
    }
    public void Save()
    {
        string gameDataFile = "Save/" + "systemsave.sav";

        string dataString = xs.SerializeObject(systemdata, typeof(SystemData));
        xs.CreateXML(gameDataFile, dataString);
    }
}
