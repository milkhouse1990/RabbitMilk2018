using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum through { None, Right, Left, Up, Down }
public class Rect4
{
    public float left;
    public float right;
    public float up;
    public float down;
    public Rect4(float p_up, float p_down, float p_left, float p_right)
    {
        up = p_up;
        down = p_down;
        left = p_left;
        right = p_right;
    }
    public Rect4(Rect room)
    {
        left = room.x;
        right = room.x + room.width;
        up = room.y;
        down = room.y - room.height;
    }
    public Rect4 Local2World(Transform transform)
    {
        Rect4 collider = new Rect4(transform.position.y + up, transform.position.y + down, transform.position.x + left, transform.position.x + right);
        return collider;
    }
}
public class CameraFollow : MonoBehaviour
{
    // some constants
    static float tiles = 64f;
    static float xtiles = 1280f / tiles;
    static float ytiles = 720f / tiles;
    public int CameraMode = 0;
    public Rect[] Rooms;
    private Rect4 CurrentRoom;
    private float left_border;
    private float right_border;
    private int[] scroll_door;
    private int l_door;
    private int i = 0;
    private through b_through = through.None;
    private bool b_moving = false;
    private Transform target = null;

    private Rect view_range;
    // Use this for initialization
    void Start()
    {
        switch (CameraMode)
        {
            case 0:
                target = GameObject.Find("milk").transform;
                if (target != null)
                {
                    // find current room
                    CurrentRoom = FindCurrentRoom();
                }
                else
                    Debug.Log("cannot find milk.");
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (CameraMode)
        {
            case 0:
                switch (b_through)
                {
                    case through.Right:
                        if (transform.position.x >= CurrentRoom.left + xtiles / 2)
                        {
                            b_through = through.None;
                            target.gameObject.GetComponent<Platformer2DUserControl>().enabled = true;
                        }
                        else
                            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                        break;
                    case through.Left:
                        if (transform.position.x <= CurrentRoom.right - xtiles / 2)
                        {
                            b_through = through.None;
                            target.gameObject.GetComponent<Platformer2DUserControl>().enabled = true;
                        }
                        else
                            transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                        break;
                    case through.None:
                        if (target != null)
                        {
                            // player view
                            // left
                            float PlayerViewLeft = target.position.x - 10f;
                            float RoomLeft = CurrentRoom.left;
                            if (PlayerViewLeft < RoomLeft)
                            {
                                PlayerViewLeft = RoomLeft;
                            }
                            else
                            {
                                // right
                                float PlayerViewRight = target.position.x + 10f;
                                float RoomRight = CurrentRoom.right;
                                if (PlayerViewRight > RoomRight)
                                {
                                    PlayerViewRight = RoomRight;
                                    PlayerViewLeft = PlayerViewRight - 20f;
                                }
                            }

                            // up
                            float PlayerViewUp = target.position.y + ytiles / 2;
                            float RoomUp = CurrentRoom.up;
                            if (PlayerViewUp > RoomUp)
                            {
                                PlayerViewUp = RoomUp;
                            }
                            else
                            {
                                // down
                                float PlayerViewDown = target.position.y - ytiles / 2;
                                float RoomDown = CurrentRoom.down;
                                if (PlayerViewDown < RoomDown)
                                {
                                    PlayerViewDown = RoomDown;
                                    PlayerViewUp = PlayerViewDown + ytiles;
                                }
                            }

                            transform.position = new Vector3(PlayerViewLeft + 10f, PlayerViewUp - ytiles / 2, -20);

                            // walk through right
                            if (target.position.x > CurrentRoom.right)
                            {
                                b_through = through.Right;
                                CurrentRoom = FindCurrentRoom();
                                target.gameObject.GetComponent<Platformer2DUserControl>().enabled = false;
                                target.gameObject.GetComponent<Physics2DM>().velocity = Vector3.zero;
                            }
                            // walk through left
                            if (target.position.x < CurrentRoom.left)
                            {
                                b_through = through.Left;
                                CurrentRoom = FindCurrentRoom();
                                target.gameObject.GetComponent<Platformer2DUserControl>().enabled = false;
                                target.gameObject.GetComponent<Physics2DM>().velocity = Vector3.zero;
                            }
                        }
                        break;
                }
                break;
        }
    }
    public bool GetMoving()
    {
        return b_moving;
    }
    public Rect4 FindCurrentRoom()
    {
        foreach (Rect room in Rooms)
        {
            Rect4 room4 = new Rect4(room);

            if (target.position.y < room4.up)
                if (target.position.y > room4.down)
                    if (target.position.x > room4.left)
                        if (target.position.x < room4.right)
                        {
                            return room4;
                        }
        }
        return null;
    }
}
