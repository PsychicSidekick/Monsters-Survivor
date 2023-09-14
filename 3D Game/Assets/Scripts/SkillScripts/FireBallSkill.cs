using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireBallSkill : Skill
{
    public GameObject ballPrefab;
    public float ballRange;
    public float ballSpeed;
    public float baseSpread;
    public float ballBaseDmg;
    public int numberOfProjectiles;

    public float baseExplosionRadius;
    public float baseExplosionDamage;

    public float baseIgniteDuration;
    public float baseIgniteChance;

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
        FireBallSkillTree skillTree = skillUser.GetComponent<FireBallSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();

        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        Vector3 targetDirection = (skillHandler.groundTarget - startPos).normalized;
        startPos += targetDirection;

        float fireBallDamage = (ballBaseDmg + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedDamage + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedProjectileDamage.value);
        float igniteDuration = baseIgniteDuration * (1 + skillTree.increasedIgniteDuration + skillUser.stats.increasedIgniteDuration.value);
        float igniteChance = baseIgniteChance + skillTree.increasedIgniteChance + skillUser.stats.additionalIgniteChance.value;
        int numberOfFireBalls = numberOfProjectiles + skillTree.additionalFireBalls + (int)skillUser.stats.additionalNumberOfProjectiles.value;
        float fireBallRange = ballRange * (1 + skillTree.increasedRange);
        float fireBallSpeed = ballSpeed * (1 + skillTree.increasedSpeed + skillUser.stats.increasedProjectileSpeed.value);

        float explosionRadius = baseExplosionRadius * (1 + skillTree.increasedExplosionRadius + skillUser.stats.increasedAreaEffect.value);
        float explosionDamage = baseExplosionDamage * (1 + skillTree.increasedExplosionDamage + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedAreaDamage.value);

        for (int i = 0; i < numberOfFireBalls; i++)
        {
            EffectCollider collider = Instantiate(ballPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
            Projectile proj = collider.GetComponent<Projectile>();
            ExplodingProjectile explodingProj = proj.GetComponent<ExplodingProjectile>();

            proj.targetPos = startPos + Quaternion.Euler(0, (numberOfFireBalls - 1) * -baseSpread + i * 2 * baseSpread, 0) * targetDirection * fireBallRange;
            proj.projSpeed = fireBallSpeed;

            explodingProj.explosionRadius = explosionRadius;
            explodingProj.explosionDamageType = DamageType.Fire;
            explodingProj.explosionDamage = explosionDamage;

            if (!skillTree.igniteAppliedByExplosion)
            {
                float igniteDamage = fireBallDamage * 0.5f * (1 + skillTree.increasedIgniteDamage + skillTree.increasedIgniteDuration);
                IgniteEffect ignite = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);

                collider.SetHostileEffects(fireBallDamage, DamageType.Fire, false, skillUser, null, ignite);
            }
            else
            {
                float igniteDamage = explosionDamage * 0.5f * (1 + skillTree.increasedIgniteDamage + skillTree.increasedIgniteDuration);
                IgniteEffect ignite = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);

                collider.SetHostileEffects(fireBallDamage, DamageType.Fire, false, skillUser, null);
                explodingProj.explosionStatusEffects.Add(ignite);
            }
        }
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<FireBallSkillTree>().additionalManaCost;
    }
}
