using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkillTree : SkillTree
{
    public DamageType damageType;
    public float increasedRange;
    public float increasedDamage;

    public float increasedIgniteDamage;
    public float increasedIgniteDuration;
    public float increasedIgniteChance;

    public float increasedSlowEffect;
    public float increasedSlowDuration;
    public float increasedSlowChance;

    public float increasedShockEffect;
    public float increasedShockDuration;
    public float increasedShockChance;

    public override void ResetSkillTree()
    {
        Debug.Log("Not player skill");
    }
}
