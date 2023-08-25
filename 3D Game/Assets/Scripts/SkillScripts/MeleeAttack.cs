using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MeleeAttack : Skill
{
    public float baseRange;

    public override void OnUse(Character skillUser)
    {
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        skillHandler.SetCurrentAttackSpeedMod(0);
        skillUser.StopMoving();
        skillUser.animator.Play("MeleeAttack");
    }

    public override void UseSkill(Character skillUser)
    {
        Character targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;
        if (Vector3.Distance(skillUser.transform.position, targetCharacter.transform.position) <= baseRange + skillUser.GetComponent<MeleeSkillTree>().increasedRange + 1)
        {
            targetCharacter.ReceiveDamage(new Damage(500, skillUser, DamageType.Fire));
        }
    }
}
