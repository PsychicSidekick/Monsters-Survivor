using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightningSkillTree : MonoBehaviour
{
    public int additionalManaCost;

    public float increasedDamage;
    public float increasedSpread;
    public float increasedRange;
    public int additionalNumberOfProjectiles;
    public int additionalNumberOfChains;
    public float increasedChainingRange;
    public float increasedChainingDamageMultiplier;
    public float increasedShockChance;
    public float increasedShockDuration;
    public float increasedShockEffect;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseDamage(float value)
    {
        increasedDamage += value;
    }

    public void IncreaseSpread(float value)
    {
        increasedSpread += value;
    }

    public void IncreaseRange(float value)
    {
        increasedRange += value;
    }

    public void IncreaseNumberOfProjectiles(int value)
    {
        additionalNumberOfProjectiles += value;
    }

    public void IncreaseNumberOfChains(int value)
    {
        additionalNumberOfChains += value;
    }

    public void IncreaseChainingRange(float value)
    {
        increasedChainingRange += value;
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
}
