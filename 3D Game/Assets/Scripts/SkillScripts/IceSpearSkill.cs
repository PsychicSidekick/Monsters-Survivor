using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IceSpearSkill : Skill
{
    public GameObject iceSpearPrefab;

    public int baseNumberOfIceSpears;
    public int baseIceSpearPierce;
    public float baseIceSpearDamage;
    public float baseIceSpearRange;
    public float baseIceSpearSpeed;
    public float baseIceSpearSpread;

    public float baseSlowEffect;
    public float baseSlowChance;
    public float baseSlowDuration;

    public override void OnUse(Character skillUser)
    {
        IceSpearSkillTree skillTree = skillUser.GetComponent<IceSpearSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();

        skillHandler.SetCurrentAttackSpeedMod(skillTree.increasedAttackSpeed);
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

        int numberOfSpears = baseNumberOfIceSpears + skillTree.additionalNumberOfIceSpears + (int)skillUser.stats.additionalNumberOfProjectiles.value;
        int spearPierce = baseIceSpearPierce + skillTree.additionalNumberOfIceSpearPierces;
        float spearDamage = (baseIceSpearDamage * (1 + skillTree.increasedBaseIceSpearDamage) + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedIceSpearDamage + skillUser.stats.increasedColdDamage.value + skillUser.stats.increasedProjectileDamage.value);
        float spearRange = baseIceSpearRange * (1 + skillTree.increasedIceSpearRange);
        float spearSpread = baseIceSpearSpread * (1 + skillTree.increasedIceSpearSpread);
        float spearSpeed = baseIceSpearSpeed * (1 + skillTree.increasedIceSpearSpeed + skillUser.stats.increasedProjectileSpeed.value);

        float slowEffect = baseSlowEffect + skillTree.increasedSlowEffect + skillUser.stats.increasedSlowEffect.value;
        float slowChance = baseSlowChance + skillTree.increasedSlowChance + skillUser.stats.additionalSlowChance.value;
        float slowDuration = baseSlowDuration * (1 + skillTree.increasedSlowDuration + skillUser.stats.increasedSlowDuration.value);

        for (int i = 0; i < numberOfSpears; i++)
        {
            EffectCollider collider = Instantiate(iceSpearPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
            SlowEffect slow = new SlowEffect(slowEffect, slowDuration, slowChance);
            collider.SetHostileEffects(spearDamage, DamageType.Cold, false, skillUser, null, slow);

            Projectile proj = collider.GetComponent<Projectile>();
            proj.targetPos = startPos + Quaternion.Euler(0, (numberOfSpears - 1) * -spearSpread + i * 2 * spearSpread, 0) * targetDirection * spearRange;
            proj.projSpeed = spearSpeed;
            proj.pierce = spearPierce;
        }
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<IceSpearSkillTree>().additionalManaCost;
    }
}
