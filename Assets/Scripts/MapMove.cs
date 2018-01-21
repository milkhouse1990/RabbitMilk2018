using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapMove : MonoBehaviour
{

    private Transform milk;
    private Text PlaceInfo;

    private GameObject dataMenu;

    private GameObject dataCanvas;

    private Transform placesTransform;
    public int placeProgress = 0;

    void Awake()
    {
        placesTransform = transform.Find("Places");
    }
    // Use this for initialization
    void Start()
    {
        milk = transform.Find("milk");
        PlaceInfo = transform.Find("PlaceInfo").GetComponent<Text>();

        dataMenu = transform.Find("DataMenu").gameObject;
        dataMenu.SetActive(false);

        dataCanvas = transform.Find("DiaryBook").gameObject;
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

            // check which place is arrived
            string[] displayedNames = { "PRINCESS CASTLE", "PUZZLING FOREST", "HIGHWAY", "JELLY'S STADIUM", "COLA'S SECRET BASE", "SPACESHIP", "SECRET SHOP" };
            int place = 0;
            PlaceInfo.text = "";
            ColliderBox[] colliderBoxes = placesTransform.GetComponentsInChildren<ColliderBox>();
            foreach (ColliderBox colliderBox in colliderBoxes)
            {
                if (colliderBox.CheckIn(milk.position))
                {
                    PlaceInfo.text = displayedNames[place] + "\nfairy=1/1 data=0/0";
                    break;
                }
                place++;
            }
            // enter
            string[] sceneNames = { "0Castle1Outside", "1Forest0Forest", "2Highway0Highway", "3Stadium0Stadium", "4Base0Base", "5Space0Space" };
            if (Input.GetButtonDown("A") && place < colliderBoxes.Length)
            {
                GetComponentInParent<LvInitiate>().ReloadMap(sceneNames[place]);
            }
            if (Input.GetButtonDown("X"))
                dataMenu.gameObject.SetActive(true);
        }
        else if (dataMenu.gameObject.activeInHierarchy)
        {
            // data menu's control
            if (Input.GetButtonDown("A"))
                switch (dataMenu.GetComponent<ListTool>().current)
                {
                    // save
                    case 0:
                        dataMenu.SetActive(false);
                        dataCanvas.SetActive(true);
                        dataCanvas.GetComponent<DiaryBook>().SetSaveFlag(true);
                        break;
                    // load
                    case 1:
                        dataMenu.SetActive(false);
                        dataCanvas.SetActive(true);
                        dataCanvas.GetComponent<DiaryBook>().SetSaveFlag(false);
                        break;
                    // title
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

    }
    public void AddPlace(int updateProgress)
    {
        while (updateProgress > placeProgress)
        {
            placeProgress++;
            GameObject go = Resources.Load("UI/" + "Place" + placeProgress.ToString()) as GameObject;
            Instantiate(go, placesTransform);
        }
    }
}
