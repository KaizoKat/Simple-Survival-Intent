using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory Systems/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabseObject database;
    public Inventory Container;
    public bool IsOnline = false;

    public void AddItem(Item _item, int _ammount)
    {
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if(Container.Items[i].item == _item)
            {
                Container.Items[i].AddAmmount(_ammount);
                return;
            }
        }
        Container.Items.Add(new InventorySlot(_item.ID,_item, _ammount));
    }

    [ContextMenu("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath),FileMode.Open, FileAccess.Read);
            Container = (Inventory)formatter.Deserialize(stream);
            stream.Close();
        }     
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}

[System.Serializable]
public class Inventory
{
    public List<InventorySlot> Items = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    public int ammount;

    public InventorySlot(int _ID, Item _item, int _ammount)
    {
        ID = _ID;
        item = _item;
        ammount = _ammount;
    }

    public void AddAmmount(int value)
    {
        ammount += value;
    }
}
