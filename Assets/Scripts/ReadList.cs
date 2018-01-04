using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Item
{
    public string name;
    public string info;
}
public class ListItems
{
    public Item[] items;
}
public class ReadList
{
    public string[] items;
    public string[] infos;
    public ReadList(string binid)
    {
        XmlSaver xs = new XmlSaver();
        string path = "Menu/" + binid + ".xml";
        ListItems list = xs.GetInfo(path, typeof(ListItems)) as ListItems;

        int i = list.items.Length;
        items = new string[i];
        infos = new string[i];
        i = 0;
        foreach (Item it in list.items)
        {
            items[i] = it.name;
            infos[i] = it.info;
            i++;
        }
    }
}
