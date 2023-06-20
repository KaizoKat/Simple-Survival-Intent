using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory Systems/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public int Durability;

    public bool isMele;
    public bool isRange;
    public float Dammage;
    public float Range;
    public float AttackSpeed;

    public bool isTool;
    public bool Axe;
    public bool Pick;
    public bool Hoe;
    public bool Shovel;
    public int breakingLevel;
    public float toolSpeed;

    public bool isArmoor;
    public bool Helmet;
    public bool Chestplate;
    public bool Legings;
    public bool Boots;

    public bool isHelperTool;
    public bool Shield;
    public bool Lamp;

    public void Awake()
    {
        type = ItemType.Equipment;
    }
}
