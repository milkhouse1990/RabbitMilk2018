using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemList_name : MonoBehaviour {
    private int[] bag = new int[50];
    private string[] names;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 50; i++)
            bag[i] = 0;
        bag[0] = 1;
        bag[1] = 2;

        names = new string[2] { "[农具/种]胡萝卜的种子", "[农具/壶]破破烂烂的水壶" };
    }
	
	// Update is called once per frame
	void Update () {
        int current = GetComponentInParent<ListTool>().GetFocus();
        string dis;
        if (bag[current] == 0)
            dis = "";
        else
            dis = names[bag[current] - 1];

        GetComponent<Text>().text = dis;
    }
}
