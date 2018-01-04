using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Boss : MonoBehaviour
{
    // private bool ready;
    public GameObject boss_hp_guage;

    public void GetReady()
    {
        // ready = true;
        // GetComponent<Enemy1>().enabled = true;
        Instantiate(boss_hp_guage);
    }
}
