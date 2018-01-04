using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss0Berry : MonoBehaviour {
    private int skill = -1;
    private Counter c_Counter;
    private int timer = 60;
    private int counter2 = -1;
    private int a_arms=1;
    private Transform[] T_arms=new Transform[20];
    public Transform O_arm;
    private Transform t_arm;
    private bool backwards;
	// Use this for initialization
	void Start () {
		
	}
	void Awake()
    {
        T_arms[0] = transform.Find("Arm");
        c_Counter = GetComponent<Counter>();
    }
	// Update is called once per frame
	void Update () {

        //if (skill == 0)
            
                //arm.transform.position += new Vector3(-1, 0, 0);
                //T_arms[i].GetComponent<Rigidbody2D>().velocity = new Vector2(-16, 0);
        //t_arm.transform.localScale = new Vector3(-2 - 2*t_arm.transform.localPosition.x , 1, 1);
    }

    void FixedUpdate()
    {
        switch (skill)
        {
            case 0:
                for (int i = 0; i < a_arms; i++)
                if (!backwards)
                    T_arms[i].GetComponent<Rigidbody2D>().velocity = new Vector2(-24, 0);
                else
                {
                    T_arms[i].GetComponent<Rigidbody2D>().velocity = new Vector2(8, 0);
                    if (T_arms[i].localPosition.x >= -1.5f)
                        if (i == 0)
                        {
                            backwards = false;
                            skill = -1;
                                timer = 30;
                                T_arms[i].localPosition = new Vector3(-1.5f, T_arms[i].localPosition.y, 0);
                        }
                        else
                            {
                                GameObject.Destroy(T_arms[i].gameObject);
                                if (i != a_arms - 1)
                                    Debug.Log("error:arm destroyed is not the last one.");
                                a_arms--;
                            }
                            
                }

                break;
        }
        float[] probs = { 75f, 25f, 0f, 0f };
        if (timer == 0)
        {
            switch (Choose(probs))
            {
                case 0:
                    //scissors 向前方
                    skill = 0;
                    T_arms[0].GetComponent<Boss0BerryArm>().SetSkill(0);
                    //T_arms[a_arms] =Instantiate(O_arm, transform);
                    //T_arms[a_arms].localPosition = new Vector3(-1.5f, 0.5f, 0);
                    //a_arms++;
                    //timer = -1;
                    //Debug.Log("get");
                    //t_arm.transform.position += new Vector3(-1, 0, 0);
                    break;
                //case 1:
                //transform.position += new Vector3(1, 0, 0);
                //break;
                //case 2:
                //transform.position += new Vector3(0, -1, 0);
                //break;
                //case 3:
                //transform.position += new Vector3(0, 1, 0);
                default:
                    timer = 61;
                    break;
            }
            //timer = 60+1;
        }
        //if (counter2==0)
        {
            if (skill==0)
            {
                if (T_arms[a_arms-1].localPosition.x<-2.5f)
                {
                    T_arms[a_arms] = Instantiate(O_arm, transform);
                    T_arms[a_arms].localPosition = new Vector3(-1.5f, 0.5f, 0);
                    
                    T_arms[a_arms].GetComponent<Boss0BerryArm>().SetSkill(0);
                    a_arms++;
                }
                    
                //counter2 = 31;
            }
        }
        if (timer >= 0)
            timer--;
        if (counter2 >= 0)
            counter2--;
    
    }
    public void SetBackwards(bool p_backwards)
    {
        backwards = p_backwards;
    }

    
    private int Choose(float[] probs)
    {
        //将事件元素加入到数组中，如上面有4个元素，分别为50,25,20,5
        float total = 0;
        foreach (float elem in probs)
            total += elem;
        //Random.value方法返回一个0—1的随机数
        float randomPoint = Random.value * total;
        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
                return i;
            else
                randomPoint -= probs[i];
        }
        return probs.Length - 1;
    }
}
