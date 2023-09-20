using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlastSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedAttackSpeed;
    public float additionalCooldownTime;

    public float increasedFireBlastDamage;
    public float increasedFireBlastRadius;
    public float increasedFireBlastExpansionTime;

    public float increasedIgniteDamage;
    public float increasedIgniteChance;
    public float increasedIgniteDuration;

    public bool doesNotDealDamageOnHit;
    public bool destroysProjectiles;
    public bool doesNotStopToUseSkill;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseAttackSpeed(float value)
    {
        increasedAttackSpeed += value;
    }

    public void IncreaseCooldownTime(float value)
    {
        additionalCooldownTime += value;
    }

    public void IncreaseFireBlastDamage(float value)
    {
        increasedFireBlastDamage += value;
    }

    public void IncreaseFireBlastRadius(float value)
    {
        increasedFireBlastRadius += value;
    }

    public void IncreaseFireBlastExpansionTime(float value)
    {
        increasedFireBlastExpansionTime += value;
    }

    public void IncreaseIgniteDamage(float value)
    {
        increasedIgniteDamage += value;
    }

    public void IncreaseIgniteChance(float value)
    {
        increasedIgniteChance += value;
    }

    public void IncreaseIgniteDuration(float value)
    {
        increasedIgniteDuration += value;
    }

    public void ToggleDoesNotDealDamageOnHit()
    {
        doesNotDealDamageOnHit = true;
    }

    public void ToggleDestroysProjectiles()
    {
        destroysProjectiles = true;
    }

    public void ToggleDoesNotStopToUseSkill()
    {
        doesNotStopToUseSkill = true;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedAttackSpeed = 0;
        additionalCooldownTime = 0;

        increasedFireBlastDamage = 0;
        increasedFireBlastRadius = 0;
        increasedFireBlastExpansionTime = 0;

        increasedIgniteDamage = 0;
        increasedIgniteChance = 0;
        increasedIgniteDuration = 0;

        doesNotDealDamageOnHit = false;
        destroysProjectiles = false;
        doesNotStopToUseSkill = false;
    }
}
