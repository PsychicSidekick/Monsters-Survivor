using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemPrefab : ScriptableObject
{
    public string itemName;
    public ItemType type;
    public Vector2Int size;
    public GameObject itemObjPrefab;
    public GameObject itemImgPrefab;
}
