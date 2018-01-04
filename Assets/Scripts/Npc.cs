using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public string npcno;
    public Rect check;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}
