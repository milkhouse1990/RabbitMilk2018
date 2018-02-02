using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Physics2DM : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public Vector3 gravity = new Vector3(0, -30, 0);
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

        // check ground

        // check rampup
        RampUp[] rampUps = FindObjectsOfType<RampUp>() as RampUp[];
        foreach (RampUp rampUp in rampUps)
        {
            Vector3 foot = transform.position + new Vector3(-mcb.size.width / 2, -ImageSize.height / 2, 0);
            for (int i = 0; i < mcb.size.width * 10 + 1; i++)
            {
                if (rampUp.CheckIn(foot))
                {
                    {
                        transform.position += Vector3.up * rampUp.GetRise(transform.position + new Vector3(mcb.size.width / 2, -ImageSize.height / 2, 0));
                        velocity -= Vector3.up * velocity.y;
                        mGrounded = true;
                        mRamp = true;
                        // Debug.Log("OnRampUp");
                    }
                    break;
                }
                else
                    foot += new Vector3(0.1f, 0, 0);
            }
        }

        // check colliderbox
        ColliderBox[] cbs = FindObjectsOfType<ColliderBox>() as ColliderBox[];
        foreach (ColliderBox cb in cbs)
        {
            if (cb.transform == transform)
                continue;

            Vector3 foot = transform.position + new Vector3(-mcb.size.width / 2, -ImageSize.height / 2, 0);
            for (int i = 0; i < mcb.size.width * 10 + 1; i++)
            {
                if (cb.CheckIn(foot))
                {
                    transform.position += Vector3.up * (cb.transform.position.y + (cb.size.height + ImageSize.height) / 2 - transform.position.y);
                    velocity -= Vector3.up * velocity.y;
                    mGrounded = true;
                    // check vertical moving platform
                    if (cb.name == "FallingWood")
                        transform.parent = cb.transform;
                    break;
                }
                else
                    foot += new Vector3(0.1f, 0, 0);
                // leave vertical moving platform
                if (transform.parent != null && transform.parent == cb.transform)
                    transform.parent = null;
            }
        }

        // x axis
        // player move
        transform.position += Vector3.right * velocity.x * Time.deltaTime;
        if (mRamp && velocity.x < 0)
        {
            transform.position += Vector3.up * velocity.x * Time.deltaTime;
            // mRamp = false;
        }

        // check rampup
        foreach (RampUp rampUp in rampUps)
        {
            // check one-side
            for (int dir = -1; dir < 2; dir += 2)
            {
                Vector3 left = transform.position + new Vector3(dir * mcb.size.width / 2, -ImageSize.height / 2, 0);
                for (int i = 0; i < mcb.size.height * 5 + 1; i++)
                {
                    if (rampUp.CheckIn(left))
                    {
                        transform.position += Vector3.up * rampUp.GetRise(left);
                        break;
                    }
                    else
                        left += new Vector3(0, 0.2f, 0);
                }

            }
        }
        // check colliderbox
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
                        transform.position += Vector3.right * (cb.transform.position.x - dir * (cb.size.width + mcb.size.width) / 2 - transform.position.x);
                        velocity -= Vector3.right * velocity.x;
                        // Debug.Log("hit " + cb.name);
                        break;
                    }
                    else
                        left += new Vector3(0, 0.2f, 0);
                }
            }
        }

    }
}