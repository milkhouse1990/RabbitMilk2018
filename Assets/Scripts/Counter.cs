using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CounterAction();
public class Counter
{
    // -2: forever
    public int alarmTime;
    public CounterAction onCounting;
    public CounterAction onCountFinishing;
    public CounterAction onCountEnding;
    public int loop;
    public Counter(int time)
    {
        alarmTime = time;
        onCounting = doNothing;
        onCountFinishing = doNothing;
        onCountEnding = doNothing;
        loop = 1;
    }
    private void doNothing()
    {
        return;
    }
}
public class CounterSequence : MonoBehaviour
{
    public int phase;
    public int counter;
    public Counter[] counters;
    public bool over = false;
    void Start()
    {
        phase = 0;
    }
    void Update()
    {
        // counting
        if (counter > 0)
        {
            counter--;
            counters[phase].onCounting();
        }
        // count forever
        if (counter == -2)
            counters[phase].onCounting();
        // waiting for next count
        else if (counter == -1)
        {
            if (GameObject.FindWithTag("enemy") == null)
                counter = 60;
        }
        // counter end
        else if (counter == 0)
        {
            counters[phase].onCountFinishing();
            counters[phase].loop--;
            if (counters[phase].loop > 0)
            {
                if (phase == 0)
                    counter = -1;
                counter = counters[phase].alarmTime;
            }
            else
            {
                phase++;
                if (phase == counters.Length)
                {
                    phase = 0;
                    Destroy(this);
                }
                else
                    counter = counters[phase].alarmTime;
            }
        }
    }
}
