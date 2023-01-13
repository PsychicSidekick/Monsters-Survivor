using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;
    public Vector2Int size;
    public ItemType type;

    public ItemPrefab itemPrefab;
    public GameObject lootObj;
    public GameObject itemImg;

    public Cell occupies;

    public Item(ItemPrefab _itemPrefab)
    {
        itemPrefab = _itemPrefab;
        name = itemPrefab.name;
        size = itemPrefab.size;
        type = itemPrefab.type;
    }
}
