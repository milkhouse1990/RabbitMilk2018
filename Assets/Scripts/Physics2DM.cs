using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Physics2DM : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    private Vector3 gravity = new Vector3(0, -30, 0);
    private ColliderBox mcb;
    private Rect ImageSize = new Rect(-1, -1.5f, 2, 3);
    public bool mGrounded = false;
    public bool mRamp = false;
    void Awake()
    {
        mcb = GetComponent<ColliderBox>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mRamp = false;
        // y axis
        velocity += new Vector3(0, gravity.y * Time.deltaTime, 0);
        transform.position += new Vector3(0, velocity.y * Time.deltaTime, 0);

        ColliderBox[] cbs = FindObjectsOfType<ColliderBox>() as ColliderBox[];
        foreach (ColliderBox cb in cbs)
        {
            if (cb.transform == transform)
                continue;

            // check ground
            Vector3 foot = transform.position + new Vector3(-mcb.size.width / 2, -ImageSize.height / 2, 0);
            for (int i = 0; i < mcb.size.width * 10 + 1; i++)
            {
                if (cb.CheckIn(foot))
                {
                    if (cb.type == 1)
                    {
                        transform.position += Vector3.up * cb.GetRise(transform.position + new Vector3(mcb.size.width / 2, -ImageSize.height / 2, 0));
                        velocity -= Vector3.up * velocity.y;
                        mGrounded = true;
                        mRamp = true;
                    }
                    else
                    {
                        transform.position += Vector3.up * (cb.transform.position.y + (cb.size.height + ImageSize.height) / 2 - transform.position.y);
                        velocity -= Vector3.up * velocity.y;
                        mGrounded = true;

                    }
                    break;
                }
                else
                    foot += new Vector3(0.1f, 0, 0);
            }
        }

        // x axis
        transform.position += Vector3.right * velocity.x * Time.deltaTime;
        if (mRamp && velocity.x < 0)
        {
            transform.position += Vector3.up * velocity.x * Time.deltaTime;
            // mRamp = false;
        }

        cbs = FindObjectsOfType<ColliderBox>() as ColliderBox[];
        foreach (ColliderBox cb in cbs)
        {
            if (cb.transform == transform)
                continue;

            // check left
            for (int dir = -1; dir < 2; dir += 2)
            {
                Vector3 left = transform.position + new Vector3(dir * mcb.size.width / 2, -ImageSize.height / 2, 0);
                for (int i = 0; i < mcb.size.height * 5 + 1; i++)
                {
                    if (cb.CheckIn(left))
                    {
                        if (cb.name == "rampright")
                        {
                            transform.position += Vector3.up * cb.GetRise(left);
                            Debug.Log("hit " + cb.name);
                            break;
                        }
                        else
                        {
                            transform.position += Vector3.right * (cb.transform.position.x - dir * (cb.size.width + mcb.size.width) / 2 - transform.position.x);
                            velocity -= Vector3.right * velocity.x;
                            Debug.Log("hit " + cb.name);
                            break;
                        }
                    }
                    else
                        left += new Vector3(0, 0.2f, 0);
                }
            }
        }
        // check enemy
        Enemy[] enemies = FindObjectsOfType<Enemy>() as Enemy[];
        foreach (Enemy enemy in enemies)
        {
            if (enemy.CheckIn(mcb))
            {
                GetComponent<Platformer2DUserControl>().MeetEnemy(enemy.gameObject);
                // GameObject.Destroy(enemy.gameObject);
                break;

            }
        }
        // check plot
        Plot[] plots = FindObjectsOfType<Plot>() as Plot[];
        foreach (Plot plot in plots)
        {
            if (transform.position.x > plot.transform.position.x)
            {
                if (plot.name == "GotoPlot")
                {
                    string binid = "PLOT" + plot.plotno;
                    GetComponent<Platformer2DUserControl>().EnterAVGMode(binid);
                    GameObject.Destroy(plot.gameObject);
                    break;
                }
                else if (plot.name == "GotoScene")
                    SceneManager.LoadScene(plot.plotno);
            }

        }

    }
}