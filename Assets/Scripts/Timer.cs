using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float timer;
    int seconds;
    // Use this for initialization
    void Start()
    {
        timer = 30;
        seconds = 0;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        seconds = (int)(timer);
        Debug.Log("Seconds" + seconds);
        if (seconds <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
