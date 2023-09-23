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
    public bool doesNotPierce;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseCooldownTime(float value)
    {
        additionalCooldownTime += value;
    }

    public void IncreaseAttackSpeed(float value)
    {
        increasedAttackSpeed += value;
    }

    public void IncreaseNumberOfLightningOrbs(int value)
    {
        additionalNumberOfLightningOrbs += value;
    }

    public void IncreaseLightningOrbDamage(float value)
    {
        increasedLightningOrbDamage += value;
    }

    public void IncreaseBaseLightningOrbDamage(float value)
    {
        increasedBaseLightningOrbDamage += value;
    }

    public void IncreaseLightningOrbRotationRadius(float value)
    {
        increasedLightningOrbRotationRadius += value;
    }

    public void IncreaseLightningOrbSpeed(float value)
    {
        increasedLightningOrbSpeed += value;
    }

    public void IncreaseLightningOrbDuration(float value)
    {
        increasedLightningOrbDuration += value;
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

    public void ToggleDoesNotStopMoving()
    {
        doesNotStopMoving = true;
    }

    public void ToggleRandomRotationRadius()
    {
        randomRotationRadius = true;
    }

    public void ToggleDoesNotPierce()
    {
        doesNotPierce = true;
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
        doesNotPierce = false;
}
}
