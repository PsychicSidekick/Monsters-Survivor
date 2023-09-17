using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillingGroundSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedDamage;
    public float increasedRadius;
    public float increasedDuration;
    public float increasedSlowEffect;

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

    public void IncreaseSlowEffect(float value)
    {
        increasedSlowEffect += value;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedDamage = 0;
        increasedRadius = 0;
        increasedDuration = 0;
        increasedSlowEffect = 0;
    }
}
