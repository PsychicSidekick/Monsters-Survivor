using System.Collections;
using System.Collections.Generic;
using System;

public static class RandomItemGenerator
{
    private static int minModCount = 2;
    private static int maxModCount = 4;

    private static Random random = new Random();

    public static List<StatModifier> RandomizeItemMods(ItemPrefab itemPrefab)
    {
        List<ItemModMaxValues> modPool = itemPrefab.modPool;

        List<ItemModType> existingMods = new List<ItemModType>();

        List<StatModifier> mods = new List<StatModifier>();

        // Add itemBase modifier
        BaseItemMod bsm = itemPrefab.baseItemMod;
        StatModifier baseMod = new StatModifier(bsm.itemModType, random.Next(bsm.value.x, bsm.value.y + 1));
        mods.Add(baseMod);

        // Add random number of random item modifiers
        for (int i = 0; i < random.Next(minModCount, maxModCount + 1); i++)
        {
            int itemModType;

            do
            {
                itemModType = random.Next(modPool.Count);
            } while (existingMods.Contains(modPool[itemModType].itemModType));

            existingMods.Add(modPool[itemModType].itemModType);

            int value = random.Next(1, modPool[itemModType].maxValue);

            StatModifier mod = new StatModifier(modPool[itemModType].itemModType, value);
            mods.Add(mod);
        }

        return mods;
    }
}
