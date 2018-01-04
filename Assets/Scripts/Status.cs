using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour {
    public int hpmax;
    private int hp;
    public int mpmax;
    private int mp;
    public int atk;
    public int def;

    public bool b_autorecover = false;
    private int autorecover_timer = 0;

    private bool b_dead = false;
	// Use this for initialization
	void Start () {
        hp = hpmax;
        mp = mpmax;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void FixedUpdate()
    {
        //autorecover

        if (b_autorecover)
        {
            autorecover_timer++;
            if (autorecover_timer > 30)
            {
                autorecover_timer = 0;
                hp++;

                if (hp > hpmax)
                    hp = hpmax;
            }
        }
        else
            autorecover_timer = 0;
        
    }

    public void GetDamage(Status teki)
    {
        int damage=teki.atk - def;
        HPChange(damage);    
    }
    public void HPChange(int damage)
    {
        hp -= damage;
        if (hp < 0)
            hp = 0;
        if (hp > hpmax)
            hp = hpmax;
        if (hp == 0)
        {
            b_dead = true;
            /*if (tag == "Player")
            {
                Instantiate(milk_die, transform.position, Quaternion.identity);
            }
            GameObject.Destroy(gameObject);*/
        }
    }
    public int GetHp()
    {
        return hp;
    }
    public bool GetDead()
    {
        return b_dead;
    }
    public void SetHPMax(int hpm)
    {
        hpmax = hpm;
        if (hp > hpmax)
            hp = hpmax;
    }
    public int GetMp()
    {
        return mp;
    }
}
