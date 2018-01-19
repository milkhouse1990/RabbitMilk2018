using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LevelItem
{
    public string tag;
    public string name;
    public float x;
    public float y;
    public LevelItem()
    {
        tag = "null";
        name = "null";
        x = 0;
        y = 0;
    }
}
public class EventItem
{
    public string name;
    public float x;
    public float y;
    public string arg;
    public EventItem()
    {
        name = "null";
        x = 0;
        y = 0;
        arg = "null";
    }
}
public class Background
{
    public string name;
    public float x;
    public float y;
    public Background()
    {

    }
}
// 这个类用来保存关卡配置
public class LevelInfo
{
    public List<LevelItem> items;
    public List<EventItem> events;
    public Rect[] Rooms;
    public List<Background> backgrounds;
    public LevelInfo()
    {
        items = new List<LevelItem>();
        events = new List<EventItem>();
        backgrounds = new List<Background>();
    }
}