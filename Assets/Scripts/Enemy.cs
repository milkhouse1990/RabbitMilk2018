using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Status))]
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


    }
    void FixedUpdate()
    {
        if (!GetComponent<Status>().GetDead())
        {
            Weapon[] weapons = FindObjectsOfType<Weapon>() as Weapon[];
            foreach (Weapon weapon in weapons)
            {
                if (CheckIn(new Rect4(weapon.check).Local2World(weapon.transform)))
                {
                    GetComponent<Status>().GetDamage(weapon.GetComponent<Status>());
                    if (GetComponent<Status>().GetDead())
                    {
                        Drop();
                        FallOut();
                    }
                    weapon.GetComponent<Bullet>().Init();
                }
            }
        }
    }
    public bool CheckIn(ColliderBox cb)
    {
        Rect4 player = new Rect4(cb.size).Local2World(cb.transform);
        return CheckIn(player);
    }
    private bool CheckIn(Rect4 other)
    {
        Rect4 npc = new Rect4(check).Local2World(transform);
        if (other.right <= npc.left || other.left >= npc.left || other.down >= npc.up || other.up <= npc.down)
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
        if (drop_item < 2)
        {
            GameObject dropitem = Instantiate(drop[drop_item], transform.position, Quaternion.identity);
            // delete "(Clone)" in the name
            dropitem.name = dropitem.name.Substring(0, dropitem.name.Length - 7);
        }
        ;
    }
    void FallOut()
    {
        GetComponent<Physics2DM>().velocity = new Vector2(4, 4);
        // GetComponent<Rigidbody2D>().angularVelocity = 720;
    }
}
