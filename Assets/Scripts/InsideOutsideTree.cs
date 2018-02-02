using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideOutsideTree : MonoBehaviour
{
    private bool Inside = false;
    private Transform inside_tree;
    private Transform milk;
    void Awake()
    {
        inside_tree = transform.Find("Inside");
        inside_tree.gameObject.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {
        milk = GameObject.Find("milk").transform;
        if (milk == null)
            Debug.Log("cannot find milk.");
    }

    // Update is called once per frame
    void Update()
    {
        if (Inside)
            if (milk.position.x > transform.position.x + 4)
            {
                Inside = false;
                inside_tree.gameObject.SetActive(Inside);
                Camera.main.backgroundColor = new Color(188f / 255f, 244f / 255f, 237f / 255f, 0);
            }
    }
    public void EnterTree()
    {
        Inside = !Inside;
        inside_tree.gameObject.SetActive(Inside);
        if (Inside)
            Camera.main.backgroundColor = Color.black;
        else
            Camera.main.backgroundColor = new Color(188f / 255f, 244f / 255f, 237f / 255f, 0);
    }
}
