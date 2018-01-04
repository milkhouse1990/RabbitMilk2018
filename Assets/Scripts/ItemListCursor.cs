using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListCursor : MonoBehaviour {
    private float x;
    private float y;

    public int space;
	// Use this for initialization
	void Start () {
        x = transform.position.x;
        y = transform.position.y;
        //Debug.Log(x + " " + y);
	}
	
	// Update is called once per frame
	void Update () {
        int current = GetComponentInParent<ListTool>().GetFocus();
        bool vertical = GetComponentInParent<ListTool>().vertical;

        transform.position = new Vector3(x+ (vertical ? 0 : 1) * current * 64, y - (vertical ? 1 : 0) * current * space,0);
    }
}
