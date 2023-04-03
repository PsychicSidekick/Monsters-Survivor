using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HardenSkill : Skill
{
    public override void OnUse(Character _skillUser)
    {
        base.OnUse(_skillUser);
        UseSkill();
    }

    public override void UseSkill()
    {
        skillUser.StartCoroutine(skillUser.GetComponent<StatsManager>().ApplyTemporaryBuff(new StatModifier(ItemModType.flat_armour, 1000), 5));
    }
}
