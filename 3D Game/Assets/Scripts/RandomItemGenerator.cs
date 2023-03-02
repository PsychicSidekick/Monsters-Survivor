using System.Collections;
using System.Collections.Generic;
using System;

public class RandomItemGenerator
{
    private int minModCount = 4;
    private int maxModCount = 6;

    private Random random = new Random();

    private List<int[]> maxModValues = new List<int[]>();
    
    public RandomItemGenerator()
    {
        maxModValues.Add(new int[] { 100, 15, 10});
        maxModValues.Add(new int[] { 100, 15, 10 });
        maxModValues.Add(new int[] { 100, 15, 10 });
        maxModValues.Add(new int[] { 100, 15, 10 });
        maxModValues.Add(new int[] { 3, 40, 10 });
        maxModValues.Add(new int[] { 3, 30, 10 });
        maxModValues.Add(new int[] { 10, 100, 20 });
        maxModValues.Add(new int[] { 1000, 100, 20 });
        maxModValues.Add(new int[] { 1000, 100, 20 });
        maxModValues.Add(new int[] { 40, 0, 0 });
        maxModValues.Add(new int[] { 40, 0, 0 });
        maxModValues.Add(new int[] { 40, 0, 0 });
    }

    public List<StatModifier> RandomizeItemMods(ItemPrefab itemPrefab)
    {
        List<StatMaxValues> modPool = itemPrefab.modPool;

        List<(StatType, ModType)> existingMods = new List<(StatType, ModType)>();

        List<StatModifier> mods = new List<StatModifier>();

        // Add itemBase modifier
        BaseItemMod bsm = itemPrefab.baseItemMod;
        StatModifier baseMod = new StatModifier(bsm.statType, random.Next(bsm.value.x, bsm.value.y + 1), bsm.modType);
        mods.Add(baseMod);

        // Add random number of random item modifiers
        for (int i = 0; i < random.Next(minModCount, maxModCount + 1); i++)
        {
            int statType;
            int modType;

            do
            {
                statType = random.Next(itemPrefab.modPool.Count);
                modType = random.Next(3);
            } while (existingMods.Contains((modPool[statType].statType, (ModType)modType)) || modPool[statType].maxValues[modType] <= 0);

            existingMods.Add((modPool[statType].statType, (ModType)modType));

            int value = 0;
                
            while(value == 0)
            {
                value = random.Next(modPool[statType].maxValues[modType]);
            }

            StatModifier mod = new StatModifier(modPool[statType].statType, value, (ModType)modType);
            mods.Add(mod);
        }

        return mods;
    }
}
