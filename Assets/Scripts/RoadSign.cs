using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSign : MonoBehaviour
{
    private Vector3 pos;
    private string[] label = new string[2];

    public float[] x, y;
    public float width, height;
    public GameObject exit;
    public GameObject milk;
    public GameObject sign;
    // Use this for initialization
    void Start()
    {
        //GameObject exit_left = Instantiate(exit, new Vector3(2, 2,0), Quaternion.identity);
        GameObject exit_right = Instantiate(exit, new Vector3(16, 2, 0), Quaternion.identity);
        exit_right.name = "GotoPlot";
        Plot plot = exit_right.GetComponent<Plot>();
        GameObject player = GameObject.Find("milk");
        Vector3 roadsign_pos = new Vector3(10, 5, 0);

        GameObject ACTManage = GameObject.Find("ACTManager");
        string last_scene = ACTManage.GetComponent<LvInitiate>().LastLevel;

        switch (last_scene)
        {
            case "0Castle1Outside":
                label[0] = "PRINCESS CASTLE";
                label[1] = "PUZZLING FOREST";
                //exit_left.GetComponent<GotoScene>().scenename = "0Castle1Outside";
                plot.plotno = "1";
                player.transform.position = new Vector3(4, 2, 0);
                break;
            case "1Forest0Forest":
                label[0] = "PUZZLING FOREST";
                label[1] = "HIGHWAY";
                //exit_left.GetComponent<GotoScene>().scenename = "1Forest";
                plot.plotno = "3";
                player.transform.position = new Vector3(4, 2, 0);
                break;
            case "2Highway0Highway":
                label[0] = "HIGHWAY";
                label[1] = "JELLY'S STADIUM";
                plot.plotno = "5";
                player.transform.position = new Vector3(4, 2, 0);
                break;
            case "3Stadium0Stadium":
                label[0] = "JELLY'S STADIUM";
                label[1] = "COLA'S SECRET BASE";
                plot.plotno = "7";
                player.transform.position = new Vector3(4, 2, 0);
                break;
            case "4Base0Base":
                label[0] = "COLA'S SECRET BASE";
                label[1] = "SPACE";
                plot.plotno = "9";
                player.transform.position = new Vector3(4, 2, 0);
                break;
        }
        pos = Camera.main.WorldToScreenPoint(roadsign_pos);
        for (int i = 0; i < 2; i++)
            label[i] = "<color=magenta>To\n" + label[i] + "</color>";

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        for (int i = 0; i < 2; i++)
            GUI.Label(new Rect(pos.x + x[i], pos.y + y[i], width, height), label[i]);

    }
}
