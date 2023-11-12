using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightningBoltSkill : Skill
{
    public GameObject lightningBoltPrefab;

    public int baseNumberOfLightningBolts;
    public int baseNumberOfLightningBoltChains;
    public float baseLightningBoltChainingRange;
    public float baseLightningBoltDamage;
    public float baseLightningBoltRange;
    public float baseLightningBoltSpeed;
    public float baseLightningBoltSpread;

    public float baseShockEffect;
    public float baseShockChance;
    public float baseShockDuration;

    public override void OnUse(Character skillUser)
    {
        LightningBoltSkillTree skillTree = skillUser.GetComponent<LightningBoltSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        skillHandler.SetCurrentAttackSpeedMod(skillTree.increasedAttackSpeed);
        skillUser.StopMoving();
        skillHandler.FaceGroundTarget();
        skillUser.animator.Play("Throw");
    }

    public override void UseSkill(Character skillUser)
    {
        skillUser.audioSource.PlayOneShot(skillSFX);

        LightningBoltSkillTree skillTree = skillUser.GetComponent<LightningBoltSkillTree>();

        int numberOfLightningBolts;
        if (!skillTree.maximumNumberOfLightningBoltsIsOne)
        {
            numberOfLightningBolts = baseNumberOfLightningBolts + skillTree.additionalNumberOfLightningBolts + (int)skillUser.stats.additionalNumberOfProjectiles.value;
        }
        else
        {
            numberOfLightningBolts = 1;
        }
        int numberOfLightningBoltChains = baseNumberOfLightningBoltChains + skillTree.additionalNumberOfLightningBoltChains;
        float chainingRange = baseLightningBoltChainingRange * (1 + skillTree.increasedLightningBoltChainingRange);
        float lightningBoltDamage = (baseLightningBoltDamage * (1 + skillTree.increasedBaseLightningBoltDamage) + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedLightningBoltDamage + skillUser.stats.increasedLightningDamage.value + skillUser.stats.increasedProjectileDamage.value);
        float lightningBoltRange = baseLightningBoltRange * (1 + skillTree.increasedLightningBoltRange);
        float lightningBoltSpeed = baseLightningBoltSpeed * (1 + skillTree.increasedLightningBoltSpeed + skillUser.stats.increasedProjectileSpeed.value);
        float lightningBoltSpread = baseLightningBoltSpread * (1 + skillTree.increasedLightningBoltSpread);

        float shockEffect = baseShockEffect + skillTree.increasedShockEffect + skillUser.stats.increasedShockEffect.value;
        float shockChance = baseShockChance + skillTree.increasedShockChance + skillUser.stats.additionalShockChance.value;
        float shockDuration = baseShockDuration * ( 1 + skillTree.increasedShockDuration + skillUser.stats.increasedShockDuration.value);

        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        Vector3 targetDirection = (skillUser.GetComponent<SkillHandler>().groundTarget - startPos).normalized;
        startPos = startPos + targetDirection;

        for (int i = 0; i < numberOfLightningBolts; i++)
        {
            EffectCollider collider = Instantiate(lightningBoltPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
            ShockEffect shock = new ShockEffect(shockEffect, shockDuration, shockChance);
            collider.SetHostileEffects(lightningBoltDamage, DamageType.Lightning, false, skillUser, null, shock);

            Projectile proj = collider.GetComponent<Projectile>();
            proj.targetPos = startPos + Quaternion.Euler(0, (numberOfLightningBolts - 1) * -lightningBoltSpread + i * 2 * lightningBoltSpread, 0) * targetDirection * lightningBoltRange;
            proj.projSpeed = lightningBoltSpeed;
            proj.chain = numberOfLightningBoltChains;
            proj.chainingRange = chainingRange;
            if (skillTree.maximumNumberOfLightningBoltsIsOne)
            {
                proj.chainDamageMultiplier = 1 + (0.1f * skillTree.additionalNumberOfLightningBolts + (int)skillUser.stats.additionalNumberOfProjectiles.value);
            }
            else
            {
                proj.chainDamageMultiplier = 1;
            }

            if (skillTree.chainsToUser)
            {
                collider.SetFriendlyEffects(lightningBoltDamage * 0.1f, false, skillUser, null);
                proj.chainsToUser = true;
            }
        }
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<LightningBoltSkillTree>().additionalManaCost;
    }
}
