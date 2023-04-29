using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChainLightningSkill : Skill
{
    public GameObject lightningBoltPrefab;
    public float lightningBoltSpeed;
    public float manaCost;
    public int numberOfChains;
    public float chainingRange;
    public float chainDamageMultiplier;
    public float baseDamage;

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FaceCharacterTarget();
        skillUser.animator.Play("ShootBall");
    }

    public override void UseSkill(Character skillUser)
    {
        if (skillUser.GetComponent<SkillHandler>().characterTarget == null)
        {
            return;
        }

        if (!skillUser.CheckSkillCost(manaCost))
        {
            return;
        }

        skillUser.ReduceMana(manaCost);

        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        HomingProjectile proj = Instantiate(lightningBoltPrefab, startPos, Quaternion.identity).GetComponent<HomingProjectile>();
        proj.targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;
        proj.damage += baseDamage + skillUser.GetComponent<StatsManager>().attackDmg.value;
        proj.projSpeed = lightningBoltSpeed;
        proj.chain = numberOfChains;
        proj.chainingRange = chainingRange;
        proj.chainDamageMultiplier = chainDamageMultiplier;
        proj.owner = skillUser;
    }
}
