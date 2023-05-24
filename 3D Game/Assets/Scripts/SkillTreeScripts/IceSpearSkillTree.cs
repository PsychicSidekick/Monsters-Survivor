using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpearSkillTree : MonoBehaviour
{
    public int additionalManaCost;

    public float increasedSpearDamage;
    public int additionalPierce;
    public float increasedSpearSpread;
    public int additionalNumberOfSpears;
    public float increasedFreezeChance;
    public float increasedFreezeDuration;
    public float increasedShatterChance;
    public float increasedShatterMultiplier;
    public float increasedSpearSpeed;
    public float increasedSpearRange;
    public bool shatterDoesNotRemoveFreeze;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseSpearDamage(float value)
    {
        increasedSpearDamage += value;
    }

    public void IncreasePierce(int value)
    {
        additionalPierce += value;
    }

    public void IncreaseSpearSpread(float value)
    {
        increasedSpearSpread += value;
    }

    public void IncreaseNumberOfSpears(int value)
    {
        additionalNumberOfSpears += value;
    }

    public void IncreaseFreezeChance(float value)
    {
        increasedFreezeChance += value;
    }

    public void IncreaseFreezeDuration(float value)
    {
        increasedFreezeDuration += value;
    }

    public void IncreaseShatterChance(float value)
    {
        increasedShatterChance += value;
    }

    public void IncreaseShatterMultiplier(float value)
    {
        increasedShatterMultiplier += value;
    }

    public void IncreaseSpearSpeed(float value)
    {
        increasedSpearSpeed += value;
    }

    public void IncreaseSpearRange(float value)
    {
        increasedSpearRange += value;
    }

    public void ToggleShatterDoesNotRemoveFreeze()
    {
        shatterDoesNotRemoveFreeze = true;
    }
}
