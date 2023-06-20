using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory Systems/Items/Food")]
public class FoodObject : ItemObject
{
    public int HPI;
    public int HPO;

    public int XPI;
    public int XPO;

    public int FI;
    public int FO;

    public int WI;
    public int WO;

    public void Awake()
    {
        type = ItemType.Food;
    }
}
