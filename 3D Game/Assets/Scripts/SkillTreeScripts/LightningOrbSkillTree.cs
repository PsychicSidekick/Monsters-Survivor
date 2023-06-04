using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningOrbSkillTree : SkillTree
{
    public int additionalManaCost;
    public float additionalCooldownTime;
    public float increasedDamage;
    public float increasedRadius;
    public float increasedSpeed;
    public float increasedDuration;
    public int additionalNumberOfProjectiles;
    public float increasedShockChance;
    public float increasedShockDuration;
    public float increasedShockEffect;
    public bool doesNotStopMoving;
    public bool randomRadius;
    public bool doesNotPierce;

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

    public void IncreaseSpeed(float value)
    {
        increasedSpeed += value;
    }

    public void IncreaseDuration(float value)
    {
        increasedDuration += value;
    }

    public void IncreaseNumberOfProjectiles(int value)
    {
        additionalNumberOfProjectiles += value;
    }

    public void IncreaseShockChance(float value)
    {
        increasedShockChance += value;
    }

    public void IncreaseShockDuration(float value)
    {
        increasedShockDuration += value;
    }

    public void IncreaseShockEffect(float value)
    {
        increasedShockEffect += value;
    }

    public void ToggleDoesNotStopMoving()
    {
        doesNotStopMoving = true;
    }

    public void ToggleRandomRadius()
    {
        randomRadius = true;
    }

    public void ToggleDoesNotPierce()
    {
        doesNotPierce = true;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        additionalCooldownTime = 0;
        increasedDamage = 0;
        increasedRadius = 0;
        increasedSpeed = 0;
        increasedDuration = 0;
        additionalNumberOfProjectiles = 0;
        increasedShockChance = 0;
        increasedShockDuration = 0;
        increasedShockEffect = 0;
        doesNotStopMoving = false;
        randomRadius = false;
        doesNotPierce = false;
}
}
