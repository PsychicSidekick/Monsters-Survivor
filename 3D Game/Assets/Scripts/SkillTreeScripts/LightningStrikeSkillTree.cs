using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedAttackSpeed;

    public int additionalNumberOfLightningStrikes;
    public float increasedLightningStrikeDamage;
    public float increasedbaseLightningStrikeDamage;
    public float increasedLightningStrikeRadius;
    public float increasedMaximumLightningStrikeRange;

    public float increasedShockEffect;
    public float increasedShockChance;
    public float increasedShockDuration;

    public bool doesNotStopMoving;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseAttackSpeed(float value)
    {
        increasedAttackSpeed += value;
    }

    public void IncreaseNumberOfLightningStrikes(int value)
    {
        additionalNumberOfLightningStrikes += value;
    }

    public void IncreaseLightningStrikeDamage(float value)
    {
        increasedLightningStrikeDamage += value;
    }

    public void IncreaseBaseLightningStrikeDamage(float value)
    {
        increasedbaseLightningStrikeDamage += value;
    }

    public void IncreaseLightningStrikeRadius(float value)
    {
        increasedLightningStrikeRadius += value;
    }

    public void IncreaseMaximumLightningStrikeRange(float value)
    {
        increasedMaximumLightningStrikeRange += value;
    }

    public void IncreaseShockEffect(float value)
    {
        increasedShockEffect += value;
    }

    public void IncreaseShockChance(float value)
    {
        increasedShockChance += value;
    }

    public void IncreaseShockDuration(float value)
    {
        increasedShockDuration += value;
    }

    public void ToggleDoesNotStopMoving(float value)
    {
        doesNotStopMoving = true;
    }
    
    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedAttackSpeed = 0;

        additionalNumberOfLightningStrikes = 0;
        increasedLightningStrikeDamage = 0;
        increasedbaseLightningStrikeDamage = 0;
        increasedLightningStrikeRadius = 0;
        increasedMaximumLightningStrikeRange = 0;

        increasedShockEffect = 0;
        increasedShockChance = 0;
        increasedShockDuration = 0;

        doesNotStopMoving = false;
    }
}
