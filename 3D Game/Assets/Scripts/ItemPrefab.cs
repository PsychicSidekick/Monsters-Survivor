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
public struct StatMaxValues
{
    public StatType statType;
    public int[3] maxValues;
}

[Serializable]
public struct BaseItemMod
{
    public StatType statType;
    public ModType modType;
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
    public List<StatMaxValues> modPool;
    public BaseItemMod baseItemMod;
}
