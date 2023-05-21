using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenOrbSkillTree : MonoBehaviour
{
    public int additionalManaCost;

    public float increasedOrbDamage;
    public float increasedOrbDuration;
    public float increasedOrbTravelSpeed;
    public float increasedOrbFreezeChance;

    public int additionalIcicles;
    public int additionalIciclePierce;
    public float increasedIcicleDamage;
    public float increasedIcicleRange;
    public float increasedIcicleTravelSpeed;
    public float increasedIcicleShootRate;
    public float increasedIcicleFreezeChance;

    public float increasedFreezeDuration;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseOrbDamage(float value)
    {
        increasedOrbDamage += value;
    }

    public void IncreaseOrbDuration(float value)
    {
        increasedOrbDuration += value;
    }

    public void IncreaseOrbTravelSpeed(float value)
    {
        increasedOrbTravelSpeed += value;
    }

    public void IncreaseOrbFreezeChance(float value)
    {
        increasedOrbFreezeChance += value;
    }

    public void IncreaseNumberOfIcicles(int value)
    {
        additionalIcicles += value;
    }

    public void IncreaseIciclePierce(int value)
    {
        additionalIciclePierce += value;
    }

    public void IncreaseIcicleDamage(float value)
    {
        increasedIcicleDamage += value;
    }

    public void IncreaseIcicleRange(float value)
    {
        increasedIcicleRange += value;
    }

    public void IncreaseIcicleTravelSpeed(float value)
    {
        increasedIcicleTravelSpeed += value;
    }

    public void IncreaseIcicleShootRate(float value)
    {
        increasedIcicleShootRate += value;
    }

    public void IncreaseIcicleFreezeChance(float value)
    {
        increasedIcicleFreezeChance += value;
    }

    public void IncreaseFreezeDuration(float value)
    {
        increasedFreezeDuration += value;
    }
}
