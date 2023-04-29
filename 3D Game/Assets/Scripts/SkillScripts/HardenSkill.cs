using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HardenSkill : Skill
{
    public override void OnUse(Character skillUser)
    {
        UseSkill(skillUser);
    }

    public override void UseSkill(Character skillUser)
    {
        skillUser.StartCoroutine(skillUser.GetComponent<StatsManager>().ApplyTemporaryBuff(new StatModifier(ItemModType.flat_armour, 1000), 5));
    }
}
