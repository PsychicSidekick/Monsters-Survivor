using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeAttack : Skill
{
    public float baseRange;

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        Character targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;
        Debug.Log(targetCharacter.name);
        skillUser.animator.Play("MeleeAttack");
    }

    public override void UseSkill(Character skillUser)
    {
        Debug.Log("do melee attack");
        Character targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;
        if (Vector3.Distance(skillUser.transform.position, targetCharacter.transform.position) <= baseRange + 1)
        {
            targetCharacter.ReceiveDamage(new Damage(500, skillUser, DamageType.Fire));
        }
    }
}
