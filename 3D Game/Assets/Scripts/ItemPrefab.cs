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
public struct ItemModMaxValues
{
    public ItemModType itemModType;
    public int maxValue;
}

[Serializable]
public struct BaseItemMod
{
    public ItemModType itemModType;
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
    public List<ItemModMaxValues> modPool;
    public BaseItemMod baseItemMod;
}
