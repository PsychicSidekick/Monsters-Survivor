using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IceSpearSkill : Skill
{
    public GameObject iceSpearPrefab;

    public float baseTravelSpeed;
    public float baseRange;
    public float baseSpread;
    public float baseDamage;
    public int baseNumberOfSpears;
    public int basePierce;
    public float baseFreezeChance;
    public float baseFreezeDuration;
    public float baseShatterChance;
    public float baseShatterMultiplier;

    public override void OnUse(Character skillUser)
    {
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        skillHandler.SetCurrentAttackSpeedMod(200);
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.animator.Play("Throw");
    }

    public override void UseSkill(Character skillUser)
    {
        IceSpearSkillTree skillTree = skillUser.GetComponent<IceSpearSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();

        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        Vector3 targetDirection = (skillHandler.groundTarget - startPos).normalized;
        startPos += targetDirection;

        // set skill values from skillTree
        int numberOfSpears = baseNumberOfSpears + skillTree.additionalNumberOfSpears;
        float spearDamage = baseDamage * (1 + skillTree.increasedSpearDamage);
        float spearRange = baseRange * (1 + skillTree.increasedSpearRange);
        float spearSpread = baseSpread * (1 + skillTree.increasedSpearSpread);
        int pierce = basePierce + skillTree.additionalPierce;
        float spearTravelSpeed = baseTravelSpeed * (1 + skillTree.increasedSpearSpeed);
        float freezeChance = baseFreezeChance + skillTree.increasedFreezeChance;
        float freezeDuration = baseFreezeDuration * (1 + skillTree.increasedFreezeDuration);
        float shatterChance = baseShatterChance + skillTree.increasedShatterChance;
        float shatterMultiplier = baseShatterMultiplier + skillTree.increasedShatterMultiplier;

        for (int i = 0; i < numberOfSpears; i++)
        {
            EffectCollider collider = Instantiate(iceSpearPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
            FreezeEffect freeze = new FreezeEffect(skillUser, freezeDuration, freezeChance);
            ShatterEffect shatter = new ShatterEffect(skillUser, spearDamage, shatterMultiplier, shatterChance, skillTree.shatterDoesNotRemoveFreeze);
            collider.SetHostileEffects(spearDamage, DamageType.Cold, false, skillUser, null, freeze, shatter);

            Projectile proj = collider.GetComponent<Projectile>();
            proj.targetPos = startPos + Quaternion.Euler(0, (numberOfSpears - 1) * -spearSpread + i * 2 * spearSpread, 0) * targetDirection * spearRange;
            proj.projSpeed = spearTravelSpeed;
            proj.pierce = pierce;
        }
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<IceSpearSkillTree>().additionalManaCost;
    }
}
