using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightningBoltSkill : Skill
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
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        skillHandler.SetCurrentAttackSpeedMod(0);
        skillUser.StopMoving();
        skillHandler.FaceGroundTarget();
        skillUser.animator.Play("Throw");
    }

    public override void UseSkill(Character skillUser)
    {
        LightningBoltSkillTree skillTree = skillUser.GetComponent<LightningBoltSkillTree>();

        int numberOfProjectiles = baseNumberOfProjectiles + skillTree.additionalNumberOfLightningBolts;
        float damage = baseDamage * (1 + skillTree.increasedLightningBoltDamage);
        float range = baseRange * (1 + skillTree.increasedLightningBoltRange);
        float projectileSpread = baseProjectileSpread * (1 + skillTree.increasedLightningBoltSpread);
        int chains = baseNumberOfChains + skillTree.additionalLightningBoltChains;
        float chainingRange = baseChainingRange * (1 + skillTree.increasedLightningBoltChainingRange);
        float chainingDamageMultiplier = baseChainDamageMultiplier + skillTree.increasedChainingDamageMultiplier;
        float shockChance = baseShockChance + skillTree.increasedShockChance;
        float shockDuration = baseShockDuration * ( 1 + skillTree.increasedShockDuration);
        float shockEffect = baseShockEffect + skillTree.increasedShockEffect;

        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        Vector3 targetDirection = (skillUser.GetComponent<SkillHandler>().groundTarget - startPos).normalized;
        startPos = startPos + targetDirection * 1.5f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            EffectCollider collider = Instantiate(lightningBoltPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
            ShockEffect shock = new ShockEffect(shockEffect, shockDuration, shockChance);
            collider.SetHostileEffects(damage, DamageType.Lightning, false, skillUser, null, shock);

            Projectile proj = collider.GetComponent<Projectile>();
            proj.targetPos = startPos + Quaternion.Euler(0, (numberOfProjectiles - 1) * -projectileSpread + i * 2 * projectileSpread, 0) * targetDirection * range;
            proj.projSpeed = projectileSpeed;
            proj.chain = chains;
            proj.chainingRange = chainingRange;
            proj.chainDamageMultiplier = chainingDamageMultiplier;

            if (skillTree.chainsToUser)
            {
                collider.SetFriendlyEffects(20, false, skillUser, null);
                collider.affectsFriendlyCharacters = true;
                proj.chainsToUser = true;
            }
        }
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<LightningBoltSkillTree>().additionalManaCost;
    }
}
