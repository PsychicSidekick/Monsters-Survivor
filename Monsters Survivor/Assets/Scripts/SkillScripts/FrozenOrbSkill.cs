using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FrozenOrbSkill : Skill
{
    public GameObject frozenOrbPrefab;
    public GameObject iciclePrefab;

    public float baseFrozenOrbDuration;
    public float baseFrozenOrbSpeed;

    public float baseFrozenOrbSlowEffect;

    public int baseNumberOfIcicles;
    public int baseIciclePierce;
    public float baseIcicleDamage;
    public float baseIcicleRange;
    public float baseIcicleSpeed;
    public float baseIcicleShootRate;

    public float baseIcicleSlowEffect;
    public float baseIcicleSlowChance;
    public float baseIcicleSlowDuration;

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
        FrozenOrbSkillTree skillTree = skillUser.GetComponent<FrozenOrbSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();

        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        Vector3 targetDirection = (skillHandler.groundTarget - startPos).normalized;
        startPos += targetDirection;

        EffectCollider frozenOrbCollider = Instantiate(frozenOrbPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();

        float frozenOrbDuration = baseFrozenOrbDuration * (1 + skillTree.increasedFrozenOrbDuration);
        float frozenOrbSpeed = baseFrozenOrbSpeed * (1 + skillTree.increasedFrozenOrbSpeed);

        float frozenOrbSlowEffect = baseFrozenOrbSlowEffect + skillTree.increasedFrozenOrbSlowEffect + skillUser.stats.increasedSlowEffect.value;

        SlowEffect slow = new SlowEffect(frozenOrbSlowEffect, 10000, 100);
        StatusEffect[] inAreaStatusEffects = { slow };
        frozenOrbCollider.SetHostileEffects(0, DamageType.Cold, false, skillUser, inAreaStatusEffects);

        Projectile frozenOrbProjectile = frozenOrbCollider.GetComponent<Projectile>();
        TimedProjectile frozenOrbTimedProjectile = frozenOrbCollider.GetComponent<TimedProjectile>();

        frozenOrbTimedProjectile.travelDirection = targetDirection;
        frozenOrbTimedProjectile.lifeTime = frozenOrbDuration;
        frozenOrbProjectile.projSpeed = frozenOrbSpeed;
        frozenOrbProjectile.pierce = 10000;

        frozenOrbCollider.StartCoroutine(ShootIcicles(frozenOrbProjectile, skillUser));
    }

    IEnumerator ShootIcicles(Projectile frozenOrbProjectile, Character skillUser)
    {
        FrozenOrbSkillTree skillTree = skillUser.GetComponent<FrozenOrbSkillTree>();

        int numberOfIcicles = baseNumberOfIcicles + skillTree.additionalNumberOfIcicles + (int)skillUser.stats.additionalNumberOfProjectiles.value;
        if (numberOfIcicles <= 0)
        {
            yield return null;
        }
        int iciclePierce = baseIciclePierce + skillTree.additionalIciclePierce;
        float icicleDamage = (baseIcicleDamage * (1 + skillTree.increasedBaseIcicleDamage) + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedIcicleDamage + skillUser.stats.increasedColdDamage.value + skillUser.stats.increasedProjectileDamage.value);
        float icicleRange = baseIcicleRange * (1 + skillTree.increasedIcicleRange);
        float icicleSpeed = baseIcicleSpeed * (1 + skillTree.increasedIcicleSpeed + skillUser.stats.increasedProjectileSpeed.value);
        float icicleShootRate = baseIcicleShootRate * (1 + skillTree.increasedIcicleShootRate);

        float icicleSlowEffect = baseIcicleSlowEffect + skillTree.increasedIcicleSlowEffect + skillUser.stats.increasedSlowEffect.value;
        float icicleSlowChance = baseIcicleSlowChance + skillTree.increasedIcicleSlowChance + skillUser.stats.additionalSlowChance.value;
        float icicleSlowDuration = baseIcicleSlowDuration + skillTree.increasedIcicleSlowDuration + skillUser.stats.increasedSlowDuration.value;

        while (frozenOrbProjectile != null)
        {
            if (skillUser.audioSource != null)
            {
                skillUser.audioSource.PlayOneShot(skillSFX);
            }

            for (int i = 0; i < numberOfIcicles; i++)
            {
                Vector3 startPos = GameManager.instance.RefinedPos(frozenOrbProjectile.transform.position);
                EffectCollider icicleCollider = Instantiate(iciclePrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();

                SlowEffect slow = new SlowEffect(icicleSlowEffect, icicleSlowDuration, icicleSlowChance);
                icicleCollider.SetHostileEffects(icicleDamage, DamageType.Cold, false, skillUser, null, slow);

                Projectile icicleProjectile = icicleCollider.GetComponent<Projectile>();
                Vector3 targetPos = (Quaternion.AngleAxis(i * 360 / numberOfIcicles, frozenOrbProjectile.transform.up) * new Vector3(1, 0, 0)) + startPos;
                Vector3 direction = Vector3.Normalize(targetPos - startPos);

                icicleProjectile.targetPos = startPos + direction * icicleRange;
                icicleProjectile.projSpeed = icicleSpeed;
                icicleProjectile.pierce = iciclePierce;
            }

            yield return new WaitForSeconds(1 / icicleShootRate);
        }
    }

    public override float OnCoolDown(Character skillUser)
    {
        return coolDownTime + skillUser.GetComponent<FrozenOrbSkillTree>().additionalCooldownTime;
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<FrozenOrbSkillTree>().additionalManaCost;
    }
}
