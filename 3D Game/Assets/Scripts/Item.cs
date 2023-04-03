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

        itemModifiers = RandomItemGenerator.RandomizeItemMods(itemPrefab);
    }

    public Item(ItemPrefab itemPrefab, List<StatModifier> savedModifiers)
    {
        name = itemPrefab.itemName;
        size = itemPrefab.size;
        type = itemPrefab.type;

        itemModifiers = savedModifiers;
    }
}
