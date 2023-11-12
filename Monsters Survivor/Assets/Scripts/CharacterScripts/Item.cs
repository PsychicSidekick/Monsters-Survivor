using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Item
{
    private static System.Random random = new System.Random();

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

        RandomiseItemModifiers();
    }

    public Item(ItemBase itemBase, List<StatModifier> savedModifiers)
    {
        name = itemBase.itemName;
        this.itemBase = itemBase;

        itemModifiers = savedModifiers;
    }

    public void RandomiseItemModifiers()
    {
        itemModifiers = 
            itemBase.modPool
            // Randomise the order of the list of possible stat modifiers
            .OrderBy(possibleMod => random.Next())
            // Pick a random number between the minimum and maximum number of stat modifiers this item base can have, then take that amount of mods from the start of the list
            .Take(random.Next(itemBase.numberOfMods.x, itemBase.numberOfMods.y + 1))
            // Reorder the list according to the original list of possible stat modifiers
            .OrderBy(possibleMod => itemBase.modPool.IndexOf(possibleMod))
            // Turn the list of chosen stat modifiers to a list of stat modifiers
            .Select(possibleMod => new StatModifier(possibleMod.itemModType, random.Next(possibleMod.minValue, possibleMod.maxValue + 1)))
            .ToList();
    }
}
