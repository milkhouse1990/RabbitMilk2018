using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Boss : MonoBehaviour
{
    // private bool ready;
    private BossHpGuage bossHpGuage;
    void Awake()
    {
        bossHpGuage = GameObject.Find("ACTManager").transform.Find("ACTCanvas").Find("BossHpGauge").GetComponent<BossHpGuage>();
    }
    public void GetReady()
    {
        bossHpGuage.gameObject.SetActive(true);
        // ready = true;
        // GetComponent<Enemy1>().enabled = true;
    }
}
