using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStrikeSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedAttackSpeed;

    public int additionalNumberOfLightningStrikes;
    public float increasedLightningStrikeDamage;
    public float increasedBaseLightningStrikeDamage;
    public float increasedLightningStrikeRadius;
    public float increasedMaximumLightningStrikeRange;

    public float increasedShockEffect;
    public float increasedShockChance;
    public float increasedShockDuration;

    public bool doesNotStopMoving;
    public bool maximumNumberOfLightningStrikesIsOne;

    public void LightningStrikeDamage()
    {
        increasedLightningStrikeDamage += 0.3f;
    }

    public void UnstableCurrents()
    {
        increasedLightningStrikeDamage += 0.5f;
        increasedMaximumLightningStrikeRange += 0.25f;
    }

    public void ManaCharged()
    {
        additionalNumberOfLightningStrikes += 5;
        additionalManaCost += 5;
    }

    public void PortablePylon()
    {
        doesNotStopMoving = true;
    }

    public void LightningRod()
    {
        maximumNumberOfLightningStrikesIsOne = true;
    }

    public void LargerArea()
    {
        increasedLightningStrikeRadius += 0.3f;
    }

    public void CastSpeed()
    {
        increasedAttackSpeed += 20;
    }

    public void PowerSplit()
    {
        additionalNumberOfLightningStrikes += 5;
        increasedBaseLightningStrikeDamage -= 0.15f;
    }

    public void ThunderStorm()
    {
        increasedLightningStrikeRadius += 1;
        additionalManaCost += 10;
    }

    public void RapidStrikes()
    {
        increasedAttackSpeed += 50;
        increasedBaseLightningStrikeDamage -= 0.2f;
    }

    public void ReducedCosts()
    {
        additionalManaCost -= 5;
    }

    public void ShockChance()
    {
        increasedShockChance += 20;
    }
    
    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedAttackSpeed = 0;

        additionalNumberOfLightningStrikes = 0;
        increasedLightningStrikeDamage = 0;
        increasedBaseLightningStrikeDamage = 0;
        increasedLightningStrikeRadius = 0;
        increasedMaximumLightningStrikeRange = 0;

        increasedShockEffect = 0;
        increasedShockChance = 0;
        increasedShockDuration = 0;

        doesNotStopMoving = false;
    }
}
