using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashSkill : Skill
{
    public override void OnUse(Character _skillUser)
    {
        base.OnUse(_skillUser);
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        UseSkill();
    }

    public override void UseSkill()
    {
        //skillUser.GetComponent<BuffManager>().ApplyBuff(new SlowBuff(50, 5));
        skillUser.GetComponent<BuffManager>().ApplyBuff(new FreezeBuff(1));
        Vector3 dashTarget = skillUser.transform.position + skillUser.transform.forward * 5;
        dashTarget.y = 0;
        skillUser.transform.position = dashTarget;
        skillUser.Move(dashTarget);
    }
}
