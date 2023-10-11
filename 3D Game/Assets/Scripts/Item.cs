using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;
    public GameObject itemImage;
    public ItemBase itemBase;
    public Cell occupiedCell;
    public LootGameObject lootGameObject;
    public List<StatModifier> itemModifiers = new List<StatModifier>();

    public Item(ItemBase itemBase)
    {
        name = itemBase.itemName;
        this.itemBase = itemBase;

        itemModifiers = RandomItemGenerator.RandomizeItemMods(itemBase);
    }

    public Item(ItemBase itemBase, List<StatModifier> savedModifiers)
    {
        name = itemBase.itemName;
        this.itemBase = itemBase;

        itemModifiers = savedModifiers;
    }
}
