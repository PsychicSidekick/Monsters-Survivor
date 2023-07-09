using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemType
{
    Weapon,
    Helmet,
    Body,
    Boots,
    Gloves,
    OffHand,
    Ring,
    Amulet,
    Belt,
}

[Serializable]
public struct ItemModMaxValue
{
    public StatModType itemModType;
    public int maxValue;
}

[Serializable]
public struct BaseItemMod
{
    public StatModType itemModType;
    public Vector2Int value;
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemPrefab : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public Vector2Int size;
    public GameObject itemObjPrefab;
    public GameObject itemImgPrefab;
    public List<ItemModMaxValue> modPool;
    public BaseItemMod baseItemMod;
}
