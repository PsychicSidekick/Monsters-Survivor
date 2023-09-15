using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorShowerSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedDamage;
    public float increasedRadius;
    public float increasedDuration;
    public float increasedIgniteDamage;
    public float increasedIgniteDuration;
    public float increasedIgniteChance;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseDamage(float value)
    {
        increasedDamage += value;
    }

    public void IncreaseRadius(float value)
    {
        increasedRadius += value;
    }

    public void IncreaseDuration(float value)
    {
        increasedDuration += value;
    }

    public void IncreaseIgniteDamage(float value)
    {
        increasedIgniteDamage += value;
    }

    public void IncreaseIgniteDuration(float value)
    {
        increasedIgniteDuration += value;
    }

    public void IncreaseIgniteChance(float value)
    {
        increasedIgniteChance += value;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedDamage = 0;
        increasedRadius = 0;
        increasedDuration = 0;
        increasedIgniteDamage = 0;
        increasedIgniteDuration = 0;
        increasedIgniteChance = 0;
}
}
