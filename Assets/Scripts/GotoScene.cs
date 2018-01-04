using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoScene : MonoBehaviour
{
    public GameObject pre_act_init;
    public string scenename;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject act_init = GameObject.Find("ACTInit");
            if (act_init == null)
                act_init = Instantiate(pre_act_init);
            act_init.GetComponent<LvInitiate>().ReloadMap(scenename);
            SceneManager.LoadScene(scenename);
        }
    }
}
