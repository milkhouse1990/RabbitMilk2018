using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapMove : MonoBehaviour
{

    private int place = 0;
    private Transform milk;
    private Text PlaceInfo;

    private GameObject dataMenu;

    private GameObject dataCanvas;
    // Use this for initialization
    void Start()
    {
        milk = transform.Find("milk");
        PlaceInfo = transform.Find("PlaceInfo").GetComponent<Text>();

        dataMenu = transform.Find("DataMenu").gameObject;
        dataMenu.SetActive(false);

        dataCanvas = transform.Find("DataCanvas").gameObject;
        dataCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dataMenu.gameObject.activeInHierarchy && !dataCanvas.activeInHierarchy)
        {
            // map move
            float MoveVelocityScalar = 4f;
            if (Input.GetButton("up"))
                milk.position += Vector3.up * MoveVelocityScalar;
            if (Input.GetButton("down"))
                milk.position += Vector3.down * MoveVelocityScalar;
            if (Input.GetButton("left"))
                milk.position += Vector3.left * MoveVelocityScalar;
            if (Input.GetButton("right"))
                milk.position += Vector3.right * MoveVelocityScalar;
            if (Input.GetButtonDown("A"))
                if (place == 1)
                    SceneManager.LoadScene("Palace_demo");
            if (Input.GetButtonDown("X"))
                dataMenu.gameObject.SetActive(true);
        }
        else if (dataMenu.gameObject.activeInHierarchy)
        {
            // data menu's control
            if (Input.GetButtonDown("A"))
                switch (dataMenu.GetComponent<ListTool>().current)
                {
                    case 0:
                        dataMenu.SetActive(false);
                        dataCanvas.SetActive(true);
                        break;
                    case 1:
                        break;
                    case 2:
                        SceneManager.LoadScene("Title");
                        break;
                }
            if (Input.GetButtonDown("B"))
            {
                dataMenu.SetActive(false);
            }
        }
        else
        {
            // data canvas's control
            if (Input.GetButtonDown("B"))
                dataCanvas.SetActive(false);
        }

        if (milk.position.x < 2)
        {
            place = 1;
            PlaceInfo.text = "CASTLE\nfairy=1/1 data=0/0";
        }
        else
        {
            place = 0;
            PlaceInfo.text = "";
        }
    }
}
