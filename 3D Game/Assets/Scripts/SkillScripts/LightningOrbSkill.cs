using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightningOrbSkill : Skill
{
    public GameObject lightningOrbPrefab;
    public GameObject rotationCenterPrefab;

    public float baseRotationSpeed;
    public float baseRotationRadius;
    public float baseDuration;
    public float baseDamage;
    public int baseNumberOfOrbs;
    public float baseShockChance;
    public float baseShockDuration;
    public float baseShockEffect;

    public override void OnUse(Character skillUser)
    {
        LightningOrbSkillTree skillTree = skillUser.GetComponent<LightningOrbSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();
        skillHandler.SetCurrentAttackSpeedMod(0);

        if (!skillTree.doesNotStopMoving)
        {
            skillUser.StopMoving();
            skillUser.animator.Play("Throw");
        }
        
        RotationCenter rotationCenter = Instantiate(rotationCenterPrefab, skillUser.transform.position, Quaternion.identity).GetComponent<RotationCenter>();
        float duration = baseDuration * (1 + skillTree.increasedDuration);
        float speed = baseRotationSpeed * (1 + skillTree.increasedSpeed);
        rotationCenter.lifeTime = duration;
        rotationCenter.rotationSpeed = speed;
        rotationCenter.movingTarget = skillUser.transform;

        int numberOfOrbs = baseNumberOfOrbs + skillTree.additionalNumberOfProjectiles;
        for (int i = 0; i < numberOfOrbs; i++)
        {
            float radius = baseRotationRadius * (1 + skillTree.increasedRadius);
            if (skillTree.randomRadius)
            {
                radius *= Random.Range(0.5f, 1.5f);
            }
            Vector3 spawnOffset = Quaternion.AngleAxis(i * 360 / numberOfOrbs, rotationCenter.transform.up) * new Vector3(radius, 0, 0);
            EffectCollider collider = Instantiate(lightningOrbPrefab, rotationCenter.transform).GetComponent<EffectCollider>();
            float damage = baseDamage * (1 + skillTree.increasedDamage);
            float shockChance = baseShockChance + skillTree.increasedShockChance;
            float shockDuration = baseShockDuration * (1 + skillTree.increasedShockDuration);
            float shockEffect = baseShockEffect + skillTree.increasedShockEffect;
            ShockEffect shock = new ShockEffect(shockEffect, shockDuration, shockChance);
            collider.SetEffects(damage, DamageType.Lightning, false, skillUser, null, shock);
            collider.transform.position = rotationCenter.transform.position + spawnOffset;
            Projectile proj = collider.GetComponent<Projectile>();
            if (!skillTree.doesNotPierce)
            {
                proj.pierce = 10000;
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
