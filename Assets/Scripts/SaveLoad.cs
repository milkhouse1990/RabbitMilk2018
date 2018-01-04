using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour {
    private GameDataManager bro_gamedatamanage;
	// Use this for initialization
	void Start () {
        if (transform.parent.GetComponentInChildren<GameDataManager>().save_flag)
            GetComponent<Text>().text = "SAVE";
        else
            GetComponent<Text>().text = "LOAD";
    }
	
}
