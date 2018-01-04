using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Scenario))]
public class ScenarioEditor : Editor
{
    private Scenario scenario;
    void OnEnable()
    {
        scenario = (Scenario)target;
        SceneView.onSceneGUIDelegate = ScenarioUpdate;
    }
    void ScenarioUpdate(SceneView scenview)
    {
        // text instruction
        Handles.BeginGUI();
        GUILayout.Label("press c to convert .sce files,\nwhen the scene window is activated");
        Handles.EndGUI();

        // key process
        Event e = Event.current;

        if (e.isKey)
        {
            if (e.character == 'c')
            {
                MakeBin(scenario.world);
            }
        }
    }

    void MakeBin(string MakeWorld)
    {
        string world="x";
        string scene = "x";
        string cut = "x";
        string ext = ".sce";
        //read .txt
        string path = "Scenario\\world" + MakeWorld + ext;
        string[] readins = File.ReadAllLines(path);

        path = "Text";

        //check the directory
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        Story story = new Story();
        Cut OneCut = new Cut();

        foreach (string readin in readins)
        {
            //comment
            if (readin.Length > 2)
                if (readin[0] == '/' && readin[1] == '/')
                    continue;
            {
                //split by \s
                string[] commands = readin.Split(' ');
                //execute commands
                switch (commands[0])
                {
                    //file name
                    case "world":
                        world = commands[1] + commands[2];
                        break;
                    case "scene":
                        if (scene != "x")
                        {
                            SaveStory(path, story);
                        }
                        story = new Story();
                        scene = commands[1] + commands[2];
                        path = "Text/" + world + scene + ".story";
                        break;
                    case "cut":
                        if (cut != "x")
                        {
                            story.cuts.Add(OneCut);
                            Debug.Log(cut + " success!");
                        }
                        OneCut = new Cut();
                        cut = "PLOT" + commands[1];
                        OneCut.no = cut;
                        break;
                    case "npccut":
                        if (cut != "x")
                        {
                            story.cuts.Add(OneCut);
                            Debug.Log(cut + " success!");
                        }
                        OneCut = new Cut();
                        cut = "NPC" + commands[1];
                        OneCut.no = cut;
                        break;
                    case "":
                        break;
                    //file contents
                    default:
                        if (isNpcName(commands[0]) || isCommand(commands[0]))
                        {
                            Line line = new Line();
                            line.args = commands;
                            OneCut.contents.Add(line);
                        }
                        else
                            Debug.Log("cannot understand this command: " + readin);
                        break;
                }
            }
        }
        story.cuts.Add(OneCut);
        Debug.Log(cut + " success!");

        SaveStory(path, story);

    }

    public bool isNpcName(string name)
    {
        string[] npcnames = { "皇家妹抖", "公主殿下", "牛奶酱", "草莓汁", "皇家妹抖？", "Drop", "？？？", "果酱亲", "台下", "声音A", "声音B", "果酱P" };
        foreach (string npcname in npcnames)
            if (npcname == name)
                return true;
        return false;
    }
    public bool isCommand(string name)
    {
        string[] commands = { "create", "downstairs", "EndingFastest", "charamove", "boss", "add", "plot", "gotoscene", "vibration", "costume" };
        foreach (string npcname in commands)
            if (npcname == name)
                return true;
        return false;
    }
    private void SaveStory(string path, Story story)
    {
        XmlSaver xs = new XmlSaver();
        string datastring = xs.SerializeObject(story, typeof(Story));
        xs.CreateXML(path, datastring);

        Debug.Log("save to " + path + " success!");
    }
}