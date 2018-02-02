using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tank : MonoBehaviour
{
    private int counter;
    public int phase;
    private int subphase;
    public int ammunition;
    public GameObject bomb;
    public GameObject aim;
    public GameObject missile;
    public GameObject shot;
    public GameObject arm;
    public GameObject catcher;
    private GameObject milk;
    private GameObject weapon;
    private Vector3 milk_pos;
    private float theta = -2.5f;
    private float step = 0.1f;
    private CounterSequence counterSequence1;
    private CounterSequence counterSequence2;
    // Use this for initialization
    void Start()
    {
        if ((milk = GameObject.Find("milk")) == null)
            Debug.Log("can't find milk.");
        counter = 60 * 2;
        subphase = 0;

        counterSequence1 = gameObject.AddComponent<CounterSequence>();

        counterSequence1.counters = new Counter[6];

        counterSequence1.counters[0] = new Counter(60);
        counterSequence1.counters[0].onCountFinishing = ThrowBomb;
        counterSequence1.counters[0].loop = ammunition;

        counterSequence1.counters[1] = new Counter(-2);
        counterSequence1.counters[1].onCounting = LaunchMissile;
        counterSequence1.counters[1].loop = 3;

        counterSequence1.counters[2] = new Counter(10);
        counterSequence1.counters[2].onCountFinishing = ShootShot;
        counterSequence1.counters[2].loop = 20;

        counterSequence1.counters[3] = new Counter(60);
        counterSequence1.counters[3].onCountFinishing = PreRabbitCatch;
        counterSequence1.counters[3].loop = 1;

        counterSequence1.counters[4] = new Counter(-2);
        counterSequence1.counters[4].onCounting = RabbitCatch;
        counterSequence1.counters[4].loop = 3;

        counterSequence1.counters[5] = new Counter(60);
        counterSequence1.counters[5].onCountFinishing = ThrowCatcher;
        counterSequence1.counters[5].loop = 3;

        counterSequence1.counter = counterSequence1.counters[0].alarmTime;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void ThrowBomb()
    {
        GameObject bom = Instantiate(bomb, transform.position, Quaternion.identity);
        bom.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 5f);
    }
    private void LaunchMissile()
    {
        if (counterSequence2 == null)
        {
            {
                counterSequence2 = gameObject.AddComponent<CounterSequence>();

                counterSequence2.counters = new Counter[2];

                counterSequence2.counters[0] = new Counter(60);
                counterSequence2.counters[0].onCountFinishing = Aim;

                counterSequence2.counters[1] = new Counter(60);
                counterSequence2.counters[1].onCountFinishing = Launch;

                counterSequence2.counter = counterSequence2.counters[0].alarmTime;
            }
            int sum = 0;
            foreach (Counter c in counterSequence2.counters)
                sum += c.alarmTime;
            counterSequence1.counter = sum;
        }
    }
    private void Aim()
    {
        milk_pos = milk.transform.position;
        weapon = Instantiate(aim, milk_pos, Quaternion.identity);
    }
    private void Launch()
    {
        GameObject.Destroy(weapon);
        GameObject bom = Instantiate(missile, transform.position, Quaternion.identity);
        float theta = Mathf.Atan2(milk_pos.y - transform.position.y, milk_pos.x - transform.position.x);
        bom.GetComponent<Rigidbody2D>().velocity = new Vector2(10f * Mathf.Cos(theta), 10f * Mathf.Sin(theta));
    }
    private void ShootShot()
    {
        theta += step;
        if (step > 0)
            if (theta > -1.57f)
                step = -0.1f;
        if (step < 0)
            if (theta < -3.14f)
                step = 0.1f;

        for (int i = 0; i < 5; i++)
        {
            weapon = Instantiate(shot, transform.position, Quaternion.identity);
            float theta2 = theta + (i - 2) * 15 / 57.3f;
            weapon.GetComponent<Rigidbody2D>().velocity = new Vector2(10f * Mathf.Cos(theta2), 10f * Mathf.Sin(theta2));
        }
    }
    private void PreRabbitCatch()
    {
        weapon = Instantiate(arm, new Vector3(15, 15, 0), Quaternion.identity);
    }
    private void RabbitCatch()
    {
        if (counterSequence2 == null)
        {
            {
                counterSequence2 = gameObject.AddComponent<CounterSequence>();

                counterSequence2.counters = new Counter[3];

                counterSequence2.counters[0] = new Counter(120);
                counterSequence2.counters[0].onCounting = Searchmilk;
                counterSequence2.counters[0].onCountFinishing = SearchmilkFinishing;

                counterSequence2.counters[1] = new Counter(20);
                counterSequence2.counters[1].onCountFinishing = Fall;

                counterSequence2.counters[2] = new Counter(20);
                counterSequence2.counters[2].onCountFinishing = Rise;

                counterSequence2.counter = counterSequence2.counters[0].alarmTime;
            }
            int sum = 0;
            foreach (Counter c in counterSequence2.counters)
                sum += c.alarmTime;
            counterSequence1.counter = sum;
        }
    }
    private void Searchmilk()
    {
        float dis = milk.transform.position.x - weapon.transform.position.x;
        if (Mathf.Abs(dis) > 0.1f)
            dis = Mathf.Sign(dis) * 0.1f;
        weapon.transform.position += new Vector3(dis, 0, 0);
    }
    private void SearchmilkFinishing()
    {
        weapon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -20f);
    }
    private void Fall()
    {
        weapon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20f);
    }
    private void Rise()
    {
        weapon.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    private void ThrowCatcher()
    {
        weapon = Instantiate(catcher, transform.position, Quaternion.identity);
        weapon.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 5f);
    }
}
