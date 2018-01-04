using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBox : MonoBehaviour
{
    public int type;
    public Rect size;// from origin
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool CheckIn(Vector3 position)
    {
        Rect4 collider = new Rect4(size);
        collider = new Rect4(transform.position.y + collider.up, transform.position.y + collider.down, transform.position.x + collider.left, transform.position.x + collider.right);
        switch (type)
        {
            // box
            case 0:
                if (position.x - collider.left > 0.01f)
                    if (position.x < collider.right)
                        if (position.y < collider.up)
                            if (position.y > collider.down)
                                return true;
                return false;
            // rightdown
            case 1:
                if (position.x < collider.right)
                    if (position.y > collider.down)
                        if (position.x > position.y - collider.down + collider.left)
                            return true;
                return false;

        }
        return false;
    }
    public float GetRise(Vector3 point)
    {
        switch (type)
        {
            case 1:
                Rect4 collider = new Rect4(size);
                collider = new Rect4(transform.position.y + collider.up, transform.position.y + collider.down, transform.position.x + collider.left, transform.position.x + collider.right);
                return point.x - (transform.position.x + size.x) - (point.y - transform.position.y + size.height / 2);

        }
        return 0;
    }
}
