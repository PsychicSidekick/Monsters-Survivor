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
        maxModValues.Add(new int[] { 50, 10, 5 });
        maxModValues.Add(new int[] { 50, 10, 10 });
        maxModValues.Add(new int[] { 50, 10, 5 });
    }

    public List<StatModifier> RandomizeItemMods(ItemPrefab itemPrefab)
    {
        List<StatModType> modPool = itemPrefab.modPool;

        List<(StatType, ModType)> existingMods = new List<(StatType, ModType)>();

        List<StatModifier> mods = new List<StatModifier>();

        for (int i = 0; i < random.Next(minModCount, maxModCount + 1); i++)
        {
            int statType;
            int modType;

            do
            {
                statType = random.Next(itemPrefab.modPool.Count);
                modType = random.Next(3);
            } while (existingMods.Contains((modPool[statType].statType, (ModType)modType)) || !modPool[statType].modType[modType]);

            existingMods.Add((modPool[statType].statType, (ModType)modType));

            int value = 0;
                
            while(value == 0)
            {
                value = random.Next(maxModValues[(int)modPool[statType].statType][modType]);
            }

            StatModifier mod = new StatModifier(modPool[statType].statType, value, (ModType)modType);
            mods.Add(mod);
        }

        return mods;
    }
}
