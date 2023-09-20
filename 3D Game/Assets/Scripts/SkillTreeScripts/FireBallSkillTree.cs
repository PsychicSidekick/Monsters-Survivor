using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedAttackSpeed;

    public int additionalNumberOfFireBalls;
    public float increasedFireBallDamage;
    public float increasedBaseFireBallDamage;
    public float increasedFireBallRange;
    public float increasedFireBallSpeed;

    public float increasedExplosionDamage;
    public float increasedBaseExplosionDamage;
    public float increasedExplosionRadius;

    public float increasedIgniteDamage;
    public float increasedIgniteChance;
    public float increasedIgniteDuration;

    public bool igniteAppliedByExplosion;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseAttackSpeed(float value)
    {
        increasedAttackSpeed += value;
    }

    public void IncreaseNumberOfFireBalls(int value)
    {
        additionalNumberOfFireBalls += value;
    }

    public void IncreaseFireBallDamage(float value)
    {
        increasedFireBallDamage += value;
    }

    public void IncreaseBaseFireBallDamage(float value)
    {
        increasedBaseFireBallDamage += value;
    }

    public void IncreaseFireBallRange(float value)
    {
        increasedFireBallRange += value;
    }

    public void IncreaseFireBallSpeed(float value)
    {
        increasedFireBallSpeed += value;
    }

    public void IncreaseExplosionDamage(float value)
    {
        increasedExplosionDamage += value;
    }

    public void IncreaseBaseExplosionDamage(float value)
    {
        increasedBaseExplosionDamage += 0;
    }

    public void IncreaseExplosionRadius(float value)
    {
        increasedExplosionRadius += value;
    }

    public void IncreaseIgniteDamage(float value)
    {
        increasedIgniteDamage += value;
    }

    public void IncreaseIgniteChance(float value)
    {
        increasedIgniteChance += value;
    }

    public void IncreaseIgniteDuration(float value)
    {
        increasedIgniteDuration += value;
    }

    public void ToggleIgniteAppliedByExplosion()
    {
        igniteAppliedByExplosion = true;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedAttackSpeed = 0;

        additionalNumberOfFireBalls = 0;
        increasedFireBallDamage = 0;
        increasedBaseFireBallDamage = 0;
        increasedFireBallRange = 0;
        increasedFireBallSpeed = 0;

        increasedExplosionDamage = 0;
        increasedBaseExplosionDamage = 0;
        increasedExplosionRadius = 0;

        increasedIgniteDamage = 0;
        increasedIgniteChance = 0;
        increasedIgniteDuration = 0;

        igniteAppliedByExplosion = false;
    }
}
