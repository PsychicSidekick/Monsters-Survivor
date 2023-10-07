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

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemBase : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public Vector2Int size;
    public GameObject lootGameObjectPrefab;
    public GameObject itemImagePrefab;
    public List<ItemModMaxValue> modPool;
}
