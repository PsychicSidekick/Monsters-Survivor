using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashSkill : Skill
{
    public override void OnUse(Character skillUser)
    {
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        UseSkill(skillUser);
    }

    public override void UseSkill(Character skillUser)
    {
        skillUser.GetComponent<StatusEffectManager>().ApplyStatusEffect(new FreezeBuff(1, 100));
        Vector3 dashTarget = skillUser.transform.position + skillUser.transform.forward * 5;
        dashTarget.y = 0;
        skillUser.transform.position = dashTarget;
        skillUser.Move(dashTarget);
    }
}
