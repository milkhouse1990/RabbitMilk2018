using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapMove : MonoBehaviour
{

    private Transform milk;
    private Text PlaceInfo;
    private Text instructions;

    private GameObject dataMenu;

    private GameObject dataCanvas;

    private Transform placesTransform;

    // Use this for initialization
    void Awake()
    {
        placesTransform = transform.Find("Places");

        milk = transform.Find("MilkBroom");
        PlaceInfo = transform.Find("PlaceInfo").GetComponent<Text>();
        instructions = transform.Find("Instruction").GetComponent<Text>();

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
                transform.parent.GetComponent<ModeSwitch>().lvInitiate.ReloadMap(sceneNames[place]);
                GetComponentInParent<ModeSwitch>().EnterMode("act");
            }
            // open data menu
            if (Input.GetButtonDown("X"))
            {
                dataMenu.gameObject.SetActive(true);
                instructions.text = "↑↓ Select A Confirm B Back";
            }
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
                        instructions.text = "↑↓ Select A Save B Back";
                        break;
                    // load
                    case 1:
                        dataMenu.SetActive(false);
                        dataCanvas.SetActive(true);
                        dataCanvas.GetComponent<DiaryBook>().SetSaveFlag(false);
                        instructions.text = "↑↓ Select A Load B Back";
                        break;
                    // title
                    case 2:
                        SceneManager.LoadScene("Title");
                        break;
                }
            if (Input.GetButtonDown("B"))
            {
                dataMenu.SetActive(false);
                instructions.text = "↑↓←→ Move A Enter X Data Menu";
            }
        }
        else
        {
            // data canvas's control
            if (Input.GetButtonDown("B"))
            {
                dataMenu.SetActive(true);
                dataCanvas.SetActive(false);
                instructions.text = "↑↓ Select A Confirm B Back";
            }
            if (Input.GetButtonDown("A") && !dataCanvas.GetComponent<DiaryBook>().GetSaveFlag())
            {
                instructions.text = "↑↓←→ Move A Enter X Data Menu";
            }
        }

    }
    public void PlacesUpdate(int updateProgress)
    {
        int progress = GetPlaceProgress();
        if (updateProgress > progress)
            for (progress++; progress <= updateProgress; progress++)
                Instantiate(Resources.Load("UI/Place" + progress.ToString()) as GameObject, placesTransform);
        else if (updateProgress < progress)
            for (; progress > updateProgress; progress--)
                GameObject.Destroy(placesTransform.GetChild(progress).gameObject);
    }
    public int GetPlaceProgress()
    {
        return placesTransform.childCount - 1;
    }
}
