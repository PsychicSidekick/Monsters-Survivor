using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightningOrbSkill : Skill
{
    public GameObject lightningOrbPrefab;
    public GameObject rotationCenterPrefab;

    public int baseNumberOfLightningOrbs;
    public float baseLightningOrbDamage;
    public float baseLightningOrbRotationRadius;
    public float baseLightningOrbSpeed;
    public float baseLightningOrbDuration;

    public float baseShockEffect;
    public float baseShockChance;
    public float baseShockDuration;

    public override void OnUse(Character skillUser)
    {
        LightningOrbSkillTree skillTree = skillUser.GetComponent<LightningOrbSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        skillHandler.SetCurrentAttackSpeedMod(skillTree.increasedAttackSpeed);

        if (!skillTree.doesNotStopMoving)
        {
            skillUser.StopMoving();
            skillUser.animator.Play("AreaCast");
        }

        int numberOfLightningOrbs = baseNumberOfLightningOrbs + skillTree.additionalNumberOfLightningOrbs + (int)skillUser.stats.additionalNumberOfProjectiles.value;
        float lightningOrbDamage = (baseLightningOrbDamage * (1 + skillTree.increasedBaseLightningOrbDamage) + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedLightningOrbDamage + skillUser.stats.increasedLightningDamage.value + skillUser.stats.increasedProjectileDamage.value);
        float lightningOrbSpeed = baseLightningOrbSpeed * (1 + skillTree.increasedLightningOrbSpeed + skillUser.stats.increasedProjectileSpeed.value);
        float lightningOrbDuration = baseLightningOrbDuration * (1 + skillTree.increasedLightningOrbDuration);

        float shockEffect = baseShockEffect + skillTree.increasedShockEffect + skillUser.stats.increasedShockEffect.value;
        float shockChance = baseShockChance + skillTree.increasedShockChance + skillUser.stats.additionalShockChance.value;
        float shockDuration = baseShockDuration * (1 + skillTree.increasedShockDuration + skillUser.stats.increasedShockDuration.value);

        RotationCenter rotationCenter = Instantiate(rotationCenterPrefab, skillUser.transform.position, Quaternion.identity).GetComponent<RotationCenter>();
        rotationCenter.lifeTime = lightningOrbDuration;
        rotationCenter.rotationSpeed = lightningOrbSpeed;
        rotationCenter.movingTarget = skillUser.transform;

        for (int i = 0; i < numberOfLightningOrbs; i++)
        {
            float lightningOrbRotationRadius = baseLightningOrbRotationRadius * (1 + skillTree.increasedLightningOrbRotationRadius);
            if (skillTree.randomRotationRadius)
            {
                lightningOrbRotationRadius *= Random.Range(0.7f, 1.3f);
            }

            Vector3 spawnOffset = Quaternion.AngleAxis(i * 360 / numberOfLightningOrbs, rotationCenter.transform.up) * new Vector3(lightningOrbRotationRadius, 0, 0);
            ShockEffect shock = new ShockEffect(shockEffect, shockDuration, shockChance);

            EffectCollider collider = Instantiate(lightningOrbPrefab, rotationCenter.transform).GetComponent<EffectCollider>();
            collider.SetHostileEffects(lightningOrbDamage, DamageType.Lightning, false, skillUser, null, shock);
            collider.transform.position = rotationCenter.transform.position + spawnOffset;

            if (!skillTree.doesNotPierce)
            {
                collider.GetComponent<Projectile>().pierce = 10000;
            }
        }
    }

    public override float OnCoolDown(Character skillUser)
    {
        return coolDownTime + skillUser.GetComponent<LightningOrbSkillTree>().additionalCooldownTime;
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<LightningOrbSkillTree>().additionalManaCost;
    }
}
