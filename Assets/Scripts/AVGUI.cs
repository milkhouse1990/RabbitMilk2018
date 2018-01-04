using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AVGUI : MonoBehaviour
{
    private Text line;
    private Image icon;
    private Text ui_name;
    private Image frame;
    // Use this for initialization
    void Start()
    {

    }
    void Awake()
    {
        line = transform.Find("Line").GetComponent<Text>();
        icon = transform.Find("Icon").GetComponent<Image>();
        ui_name = transform.Find("Name").GetComponent<Text>();
        frame = transform.Find("Frame").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Say(string speaker_name, string words)
    {
        string IconName = "null";
        string framename = "dialogue";
        Color VoiceColor = Color.black;
        if (speaker_name == "牛奶酱")
        {
            IconName = "milk";
            framename = "dialogue1";
            VoiceColor = Color.magenta;
        }

        // icon
        if (IconName != null)
        {
            icon.sprite = Resources.Load("UI\\icon_" + IconName, typeof(Sprite)) as Sprite;
        }
        else
            icon.sprite = null;

        // frame
        frame.sprite = Resources.Load("UI\\" + framename, typeof(Sprite)) as Sprite;
        // name
        ui_name.text = speaker_name;
        // words
        line.text = words;
        line.color = VoiceColor;
    }
}
