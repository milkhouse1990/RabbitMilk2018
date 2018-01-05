using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;
    public string info;
}
public class ListItems
{
    public Item[] items;
    public ListItems()
    { }
    public ListItems(string binid)
    {
        XmlSaver xs = new XmlSaver();
        string path = "Menu/" + binid + ".xml";
        ListItems list = xs.GetInfo(path, typeof(ListItems)) as ListItems;
        items = list.items;
    }
}
