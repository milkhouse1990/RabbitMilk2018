using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// must ordered from lower to higher
public class RampUp : MonoBehaviour
{
    // size of check box 
    private Rect size = new Rect(-0.5f, 0.5f, 1, 1);
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    // check whether or not 'position' is in 'rampUp'  
    public bool CheckIn(Vector3 position)
    {
        Rect4 collider = new Rect4(size);
        collider = new Rect4(transform.position.y + collider.up, transform.position.y + collider.down, transform.position.x + collider.left, transform.position.x + collider.right);

        if (position.x <= collider.right)
            if (position.y >= collider.down)
                if (position.x >= position.y - collider.down + collider.left)
                    return true;
        return false;
    }

    // when player is in 'rampup', rise the player.
    // param point player's right-bottom point;
    // return the rising offset
    public float GetRise(Vector3 point)
    {
        Rect4 collider = new Rect4(size);
        float rampRight = transform.position.x + collider.right;
        float playerRight = point.x;
        // get the smaller one of 'rampRight' and 'playerRight'
        if (playerRight > rampRight)
            playerRight = rampRight;
        return playerRight - (transform.position.x + size.x) - (point.y - transform.position.y + size.height / 2);
    }
}
