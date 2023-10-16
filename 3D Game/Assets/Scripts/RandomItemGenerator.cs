using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public static class RandomItemGenerator
{
    private static Random random = new Random();

    public static List<StatModifier> RandomizeItemMods(ItemBase itemBase)
    {
        List<ItemModValueBounds> modPool = itemBase.modPool;

        List<StatModType> existingMods = new List<StatModType>();

        List<StatModifier> mods = new List<StatModifier>();

        // Add random number of random item modifiers
        for (int i = 0; i < random.Next(itemBase.numberOfMods.x, itemBase.numberOfMods.y + 1); i++)
        {
            int itemModType;

            do
            {
                itemModType = random.Next(modPool.Count);
            } while (existingMods.Contains(modPool[itemModType].itemModType));

            existingMods.Add(modPool[itemModType].itemModType);
        }

        List<StatModType> modTypePool = modPool.Select(mod => mod.itemModType).ToList();
        existingMods = existingMods.OrderBy(mod => modTypePool.IndexOf(mod)).ToList();

        foreach (StatModType modType in existingMods)
        {
            int posInModPool = modPool.FindIndex(mod => mod.itemModType == modType);
            int value = random.Next(modPool[posInModPool].minValue, modPool[posInModPool].maxValue);
            StatModifier mod = new StatModifier(modType, value);
            mods.Add(mod);
        }

        return mods;
    }
}
