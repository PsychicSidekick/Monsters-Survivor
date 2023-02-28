using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;
    public Vector2Int size;
    public ItemType type;
    public List<StatModifier> itemModifiers = new List<StatModifier>();

    public Item(ItemPrefab itemPrefab)
    {
        name = itemPrefab.itemName;
        size = itemPrefab.size;
        type = itemPrefab.type;

        RandomItemGenerator r = new RandomItemGenerator();
        itemModifiers = r.RandomizeItemMods(itemPrefab);
    }
}
