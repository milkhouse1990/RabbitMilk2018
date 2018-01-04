using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSetPos : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetPos(Rect pos)
    {
        RectTransform rt = GetComponent<RectTransform>();
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, pos.x, pos.width);
        rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, pos.y, pos.height);
    }
}
