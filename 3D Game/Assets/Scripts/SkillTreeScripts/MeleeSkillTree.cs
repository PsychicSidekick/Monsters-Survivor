using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSkillTree : SkillTree
{
    public DamageType damageType;
    public float increasedRange;

    public void ChangeDamageType(DamageType _damageType)
    {
        damageType = _damageType;
    }

    public override void ResetSkillTree()
    {
        Debug.Log("Not player skill");
    }
}
