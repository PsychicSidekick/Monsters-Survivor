using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningOrbSkillTree : SkillTree
{
    public int additionalManaCost;
    public float additionalCooldownTime;
    public float increasedAttackSpeed;

    public int additionalNumberOfLightningOrbs;
    public float increasedLightningOrbDamage;
    public float increasedBaseLightningOrbDamage;
    public float increasedLightningOrbRotationRadius;
    public float increasedLightningOrbSpeed;
    public float increasedLightningOrbDuration;

    public float increasedShockEffect;
    public float increasedShockChance;
    public float increasedShockDuration;

    public bool doesNotStopMoving;
    public bool randomRotationRadius;

    public void LightningOrbDamage()
    {
        increasedLightningOrbDamage += 0.3f;
    }

    public void ShockEffect()
    {
        increasedShockEffect += 20;
    }

    public void ManaOrbs()
    {
        additionalNumberOfLightningOrbs += 2;
        additionalManaCost += 10;
    }

    public void MoreOrbs()
    {
        additionalNumberOfLightningOrbs += 6;
    }

    public void ChaosOrbs()
    {
        randomRotationRadius = true;
        increasedLightningOrbRotationRadius += 1;
        increasedBaseLightningOrbDamage += 0.2f;
    }

    public void OrbSpeed()
    {
        increasedLightningOrbSpeed += 0.3f;
    }

    public void OrbDuration()
    {
        increasedLightningOrbDuration += 0.3f;
    }

    public void ReducedCooldown()
    {
        additionalCooldownTime -= 3f;
    }

    public void CastSpeed()
    {
        increasedAttackSpeed += 30;
    }

    public void StormAdvance()
    {
        doesNotStopMoving = true;
    }

    public void ReducedCosts()
    {
        additionalManaCost -= 5;
    }

    public void ShockDuration()
    {
        increasedShockDuration += 1;
    }

    public void ShockChance()
    {
        increasedShockChance += 30;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        additionalCooldownTime = 0;
        increasedAttackSpeed = 0;

        additionalNumberOfLightningOrbs = 0;
        increasedLightningOrbDamage = 0;
        increasedBaseLightningOrbDamage = 0;
        increasedLightningOrbRotationRadius = 0;
        increasedLightningOrbSpeed = 0;
        increasedLightningOrbDuration = 0;

        increasedShockEffect = 0;
        increasedShockChance = 0;
        increasedShockDuration = 0;

        doesNotStopMoving = false;
        randomRotationRadius = false;
    }
}
