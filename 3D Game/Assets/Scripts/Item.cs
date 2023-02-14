using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;
    public Vector2Int size;
    public ItemType type;

    public Item(ItemPrefab itemPrefab)
    {
        name = itemPrefab.name;
        size = itemPrefab.size;
        type = itemPrefab.type;
    }
}
