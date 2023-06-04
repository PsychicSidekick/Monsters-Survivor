using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedRange;
    public float increasedSpeed;
    public float increasedDamage;
    public int additionalFireBalls;

    public float increasedExplosionRadius;
    public float increasedExplosionDamage;

    public float increasedIgniteDamageMultiplier;
    public float increasedIgniteDamage;
    public float increasedIgniteDuration;
    public float increasedIgniteChance;

    public bool igniteAppliedByExplosion;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseRange(float value)
    {
        increasedRange += value;
    }

    public void IncreaseSpeed(float value)
    {
        increasedSpeed += value;
    }

    public void IncreaseDamage(float value)
    {
        increasedDamage += value;
    }

    public void IncreaseNumberOfFireBalls(int value)
    {
        additionalFireBalls += value;
    }

    public void IncreaseExplosionRadius(float value)
    {
        increasedExplosionRadius += value;
    }

    public void IncreaseExplosionDamage(float value)
    {
        increasedExplosionDamage += value;
    }

    public void IncreaseIgniteDamageMultiplier(float value)
    {
        increasedIgniteDamageMultiplier += value;
    }

    public void IncreaseIgniteDamage(float value)
    {
        increasedIgniteDamage += value;
    }

    public void IncreaseIgniteDuration(float value)
    {
        increasedIgniteDuration += value;
    }

    public void IncreaseIgniteChance(float value)
    {
        increasedIgniteChance += value;
    }

    public void ToggleIgniteAppliedByExplosion()
    {
        igniteAppliedByExplosion = true;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedRange = 0;
        increasedSpeed = 0;
        increasedDamage = 0;
        additionalFireBalls = 0;

        increasedExplosionRadius = 0;
        increasedExplosionDamage = 0;

        increasedIgniteDamageMultiplier = 0;
        increasedIgniteDamage = 0;
        increasedIgniteDuration = 0;
        increasedIgniteChance = 0;

        igniteAppliedByExplosion = false;
    }
}
