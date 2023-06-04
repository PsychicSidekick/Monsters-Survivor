using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ChainLightningSkill : Skill
{
    public GameObject lightningBoltPrefab;

    public float projectileSpeed;
    public int baseNumberOfChains;
    public float baseChainingRange;
    public float baseChainDamageMultiplier;
    public float baseDamage;
    public int baseNumberOfProjectiles;
    public float baseProjectileSpread;
    public float baseRange;
    public float baseShockChance;
    public float baseShockDuration;
    public float baseShockEffect;

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.animator.Play("ShootBall");
    }

    public override void UseSkill(Character skillUser)
    {
        ChainLightningSkillTree skillTree = skillUser.GetComponent<ChainLightningSkillTree>();

        int numberOfProjectiles = baseNumberOfProjectiles + skillTree.additionalNumberOfProjectiles;
        float damage = baseDamage * (1 + skillTree.increasedDamage);
        float range = baseRange * (1 + skillTree.increasedRange);
        float projectileSpread = baseProjectileSpread * (1 + skillTree.increasedSpread);
        int chains = baseNumberOfChains + skillTree.additionalNumberOfChains;
        float chainingRange = baseChainingRange * (1 + skillTree.increasedChainingRange);
        float chainingDamageMultiplier = baseChainDamageMultiplier + skillTree.increasedChainingDamageMultiplier;
        float shockChance = baseShockChance + skillTree.increasedShockChance;
        float shockDuration = baseShockDuration * ( 1 + skillTree.increasedShockDuration);
        float shockEffect = baseShockEffect + skillTree.increasedShockEffect;

        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        Vector3 targetDirection = (skillUser.GetComponent<SkillHandler>().groundTarget - startPos).normalized;
        startPos = startPos + targetDirection * 0.5f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            EffectCollider collider = Instantiate(lightningBoltPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
            ShockEffect shock = new ShockEffect(shockEffect, shockDuration, shockChance);
            collider.SetEffects(damage, DamageType.Lightning, false, skillUser, null, shock);

            Projectile proj = collider.GetComponent<Projectile>();
            proj.targetPos = startPos + Quaternion.Euler(0, (numberOfProjectiles - 1) * -projectileSpread + i * 2 * projectileSpread, 0) * targetDirection * range;
            proj.projSpeed = projectileSpeed;
            proj.chain = chains;
            proj.chainingRange = chainingRange;
            proj.chainDamageMultiplier = chainingDamageMultiplier;
        }
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<ChainLightningSkillTree>().additionalManaCost;
    }
}
