using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedAttackSpeed;

    public int additionalNumberOfLightningBolts;
    public int additionalLightningBoltChains;
    public float increasedLightningBoltChainingRange;
    public float increasedLightningBoltDamage;
    public float increasedLightningBoltRange;
    public float increasedLightningBoltSpread;

    public float increasedChainingDamageMultiplier;

    public float increasedShockEffect;
    public float increasedShockChance;
    public float increasedShockDuration;

    public bool chainsToUser;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseDamage(float value)
    {
        increasedLightningBoltDamage += value;
    }

    public void IncreaseSpread(float value)
    {
        increasedLightningBoltSpread += value;
    }

    public void IncreaseRange(float value)
    {
        increasedLightningBoltRange += value;
    }

    public void IncreaseNumberOfProjectiles(int value)
    {
        additionalNumberOfLightningBolts += value;
    }

    public void IncreaseNumberOfChains(int value)
    {
        additionalLightningBoltChains += value;
    }

    public void IncreaseChainingRange(float value)
    {
        increasedLightningBoltChainingRange += value;
    }

    public void IncreaseChainingDamageMultiplier(float value)
    {
        increasedChainingDamageMultiplier += value;
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

    public void ToggleChainsToUser()
    {
        chainsToUser = true;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;

        increasedLightningBoltDamage = 0;
        increasedLightningBoltSpread = 0;
        increasedLightningBoltRange = 0;
        additionalNumberOfLightningBolts = 0;
        additionalLightningBoltChains = 0;
        increasedLightningBoltChainingRange = 0;
        increasedChainingDamageMultiplier = 0;
        increasedShockChance = 0;
        increasedShockDuration = 0;
        increasedShockEffect = 0;
        chainsToUser = false;
    }
}
