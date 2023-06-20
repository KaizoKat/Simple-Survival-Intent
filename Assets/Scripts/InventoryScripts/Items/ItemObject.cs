using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Equipment,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public int ID;
    public Sprite UiDisplay;
    public ItemType type;
    [TextArea(15,20)] public string description; 
}

[System.Serializable]
public class Item
{
    public string Name;
    public int ID;
    public Item(ItemObject item)
    {
        Name = item.name;
        ID = item.ID;
    }
}
