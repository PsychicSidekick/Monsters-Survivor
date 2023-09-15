using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlastSkillTree : SkillTree
{
    public int additionalManaCost;
    public float additionalCooldownTime;
    public float increasedDamage;
    public float increasedRadius;
    public float increasedExpansionTime;
    public float increasedIgniteDamage;
    public float increasedIgniteDuration;
    public float increasedIgniteChance;
    public bool doesNotDealDirectDamage;
    public bool doesNotDestroyProjectiles;
    public bool doesNotStopToUseSkill;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseCooldownTime(float value)
    {
        additionalCooldownTime += value;
    }

    public void IncreaseDamage(float value)
    {
        increasedDamage += value;
    }

    public void IncreaseRadius(float value)
    {
        increasedRadius += value;
    }

    public void IncreaseExpansionTime(float value)
    {
        increasedExpansionTime += value;
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

    public void ToggleDoesNotDealDirectDamage()
    {
        doesNotDealDirectDamage = true;
    }

    public void ToggleDoesNotDestroyProjectiles()
    {
        doesNotDestroyProjectiles = true;
    }

    public void ToggleDoesNotStopToUseSkill()
    {
        doesNotStopToUseSkill = true;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        additionalCooldownTime = 0;
        increasedDamage = 0;
        increasedRadius = 0;
        increasedExpansionTime = 0;
        increasedIgniteDamage = 0;
        increasedIgniteDuration = 0;
        increasedIgniteChance = 0;
        doesNotDealDirectDamage = false;
        doesNotDestroyProjectiles = false;
        doesNotStopToUseSkill = false;
    }
}
