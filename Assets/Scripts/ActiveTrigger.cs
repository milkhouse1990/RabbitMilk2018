﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrigger : MonoBehaviour
{
    public Rect check;
    // type
    // 0: Npc
    // 1: tree
    public int type = 0;
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
        // Debug.Log("player: " + player.up + " " + player.down + " " + player.left + " " + player.right);
        // Debug.Log("npc: " + npc.up + " " + npc.down + " " + npc.left + " " + npc.right);
        if (player.right <= npc.left || player.left >= npc.right || player.down >= npc.up || player.up <= npc.down)
            return false;
        else
            return true;
    }
}
