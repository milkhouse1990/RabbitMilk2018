using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tank : MonoBehaviour
{
    private int counter = 60 * 2;
    public int phase;
    private int subphase = 0;
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
    // Use this for initialization
    void Start()
    {
        if ((milk = GameObject.Find("milk")) == null)
            Debug.Log("can't find milk.");
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 0)
        {
            counter--;
            switch (phase)
            {
                case 3:
                    switch (subphase)
                    {
                        case 1:
                            float dis = milk.transform.position.x - weapon.transform.position.x;
                            if (Mathf.Abs(dis) > 0.1f)
                                dis = Mathf.Sign(dis) * 0.1f;
                            weapon.transform.position += new Vector3(dis, 0, 0);
                            break;
                    }
                    break;
                case 5:
                    Camera.main.transform.position += new Vector3(0.1f, 0, 0);
                    break;
            }

        }

        if (counter == -1)
            if (GameObject.FindWithTag("enemy") == null)
                counter = 60;

        if (counter == 0)
        {
            switch (phase)
            {
                case 0:
                    if (ammunition > 0)
                    {
                        counter = -1;
                        GameObject bom = Instantiate(bomb, transform.position, Quaternion.identity);
                        bom.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 5f);
                        ammunition--;
                    }
                    else
                    {
                        counter = 60;
                        ammunition = 20;
                        phase++;
                    }
                    break;
                case 1:
                    if (ammunition > 0)
                    {
                        switch (subphase)
                        {
                            case 0:
                                counter = 60;

                                milk_pos = milk.transform.position;
                                weapon = Instantiate(aim, milk_pos, Quaternion.identity);

                                subphase = 1;
                                break;
                            case 1:
                                counter = 60;

                                GameObject.Destroy(weapon);
                                GameObject bom = Instantiate(missile, transform.position, Quaternion.identity);
                                float theta = Mathf.Atan2(milk_pos.y - transform.position.y, milk_pos.x - transform.position.x);
                                bom.GetComponent<Rigidbody2D>().velocity = new Vector2(10f * Mathf.Cos(theta), 10f * Mathf.Sin(theta));

                                subphase = 0;
                                ammunition--;
                                break;
                        }

                    }
                    else
                    {
                        counter = 60;
                        ammunition = 20;
                        phase++;
                    }
                    break;
                case 2:
                    if (ammunition > 0)
                    {
                        counter = 10;

                        theta += step;
                        if (step > 0)
                            if (theta > -1.57f)
                                step = -0.1f;
                        if (step < 0)
                            if (theta < -3.14f)
                                step = 0.1f;
                        //float theta = Mathf.Atan2(milk_pos.y - transform.position.y, milk_pos.x - transform.position.x);

                        for (int i = 0; i < 5; i++)
                        {
                            weapon = Instantiate(shot, transform.position, Quaternion.identity);
                            float theta2 = theta + (i - 2) * 15 / 57.3f;
                            weapon.GetComponent<Rigidbody2D>().velocity = new Vector2(10f * Mathf.Cos(theta2), 10f * Mathf.Sin(theta2));
                        }

                        ammunition--;
                    }
                    else
                    {
                        counter = 60;
                        ammunition = 3;
                        phase++;
                        subphase = 0;
                    }
                    break;
                case 3:
                    if (ammunition > 0)
                    {
                        switch (subphase)
                        {
                            case 0://create arm
                                counter = 120;
                                weapon = Instantiate(arm, new Vector3(15, 15, 0), Quaternion.identity);
                                subphase++;
                                break;
                            case 1://search milk
                                counter = 20;
                                weapon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -20f);
                                subphase++;
                                break;
                            case 2://fall
                                counter = 20;
                                weapon.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20f);

                                subphase++;
                                break;
                            case 3://rise
                                counter = 120;
                                ammunition--;
                                weapon.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                                subphase = 1;
                                break;


                        }
                    }
                    else
                    {
                        counter = 60;
                        ammunition = 3;
                        phase++;
                        //subphase = 0;
                    }
                    break;
                case 4:
                    if (ammunition > 0)
                    {
                        counter = 60;
                        weapon = Instantiate(catcher, transform.position, Quaternion.identity);
                        weapon.GetComponent<Rigidbody2D>().velocity = new Vector2(-10f, 5f);
                        ammunition--;
                    }
                    else
                    {
                        counter = 60 * 1000;
                        ammunition = 3;
                        SceneManager.LoadScene("3Stadium");
                        phase++;
                    }
                    break;
                case 5:

                    break;
            }

        }


    }
}
