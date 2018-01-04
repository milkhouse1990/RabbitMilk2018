using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public Rect check;
    public GameObject[] drop;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Status>().GetDead())
        {
            Drop();
            FallOut();
        }

    }
    public bool CheckIn(ColliderBox cb)
    {
        Rect4 player = new Rect4(cb.size).Local2World(cb.transform);
        Rect4 npc = new Rect4(check).Local2World(transform);
        if (player.right <= npc.left || player.left >= npc.left || player.down >= npc.up || player.up <= npc.down)
            return false;
        else
            return true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("get");
        switch (other.tag)
        {
            case "weapon":
                GetComponent<Status>().GetDamage(other.gameObject.GetComponent<Status>());
                other.GetComponent<Bullet>().Init();
                break;

        }
    }
    void Drop()
    {
        float[] probs = new float[] { 25, 25, 0 };//heart crystal nothing
        probs[0] += PlayerPrefs.GetInt("DropHeart", 0);
        probs[1] += PlayerPrefs.GetInt("DropCrystal", 0);
        probs[2] = 100 - probs[0] - probs[1];
        RandomGroup rg = new RandomGroup(probs);
        int drop_item = rg.RandomChoose();
        // if (drop_item < 2)
        // Instantiate(drop[drop_item], transform.position, Quaternion.identity);
        ;
    }
    void FallOut()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(4, 4);
        GetComponent<Rigidbody2D>().angularVelocity = 720;
    }
}
