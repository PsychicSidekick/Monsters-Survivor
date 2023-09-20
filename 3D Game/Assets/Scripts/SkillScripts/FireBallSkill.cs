using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireBallSkill : Skill
{
    public GameObject fireBallPrefab;

    public int baseNumberOfFireBalls;
    public float baseFireBallDamage;
    public float baseFireBallRange;
    public float baseFireBallSpeed;
    public float baseFireBallSpread;

    public float baseExplosionDamage;
    public float baseExplosionRadius;

    public float baseIgniteChance;
    public float baseIgniteDuration;

    public override void OnUse(Character skillUser)
    {
        FireBallSkillTree skillTree = skillUser.GetComponent<FireBallSkillTree>();
        SkillHandler skillHandler = skillUser.GetComponent<SkillHandler>();

        skillHandler.SetCurrentAttackSpeedMod(skillTree.increasedAttackSpeed);
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

        int numberOfFireBalls = baseNumberOfFireBalls + skillTree.additionalNumberOfFireBalls + (int)skillUser.stats.additionalNumberOfProjectiles.value;
        float fireBallDamage = (baseFireBallDamage * (1 + skillTree.increasedBaseFireBallDamage) + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedFireBallDamage + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedProjectileDamage.value);
        float fireBallRange = baseFireBallRange * (1 + skillTree.increasedFireBallRange);
        float fireBallSpeed = baseFireBallSpeed * (1 + skillTree.increasedFireBallSpeed + skillUser.stats.increasedProjectileSpeed.value);

        float explosionDamage = (baseExplosionDamage * (1 + skillTree.increasedBaseExplosionDamage) + skillUser.stats.attackDamage.value) * (1 + skillTree.increasedExplosionDamage + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedAreaDamage.value);
        float explosionRadius = baseExplosionRadius * (1 + skillTree.increasedExplosionRadius + skillUser.stats.increasedAreaEffect.value);

        float increasedIgniteDamage = 1 + skillTree.increasedIgniteDamage + skillTree.increasedIgniteDuration + skillUser.stats.increasedFireDamage.value + skillUser.stats.increasedIgniteDamage.value + skillUser.stats.increasedIgniteDuration.value;
        float igniteChance = baseIgniteChance + skillTree.increasedIgniteChance + skillUser.stats.additionalIgniteChance.value;
        float igniteDuration = baseIgniteDuration * (1 + skillTree.increasedIgniteDuration + skillUser.stats.increasedIgniteDuration.value);

        for (int i = 0; i < numberOfFireBalls; i++)
        {
            EffectCollider collider = Instantiate(fireBallPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
            Projectile projectile = collider.GetComponent<Projectile>();
            ExplodingProjectile explodingProjectile = projectile.GetComponent<ExplodingProjectile>();

            projectile.targetPos = startPos + Quaternion.Euler(0, (numberOfFireBalls - 1) * -baseFireBallSpread + i * 2 * baseFireBallSpread, 0) * targetDirection * fireBallRange;
            projectile.projSpeed = fireBallSpeed;

            explodingProjectile.explosionRadius = explosionRadius;
            explodingProjectile.explosionDamageType = DamageType.Fire;
            explodingProjectile.explosionDamage = explosionDamage;

            if (!skillTree.igniteAppliedByExplosion)
            {
                float igniteDamage = fireBallDamage * 0.5f * increasedIgniteDamage;
                IgniteEffect ignite = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);

                collider.SetHostileEffects(fireBallDamage, DamageType.Fire, false, skillUser, null, ignite);
            }
            else
            {
                float igniteDamage = explosionDamage * 0.5f * increasedIgniteDamage;
                IgniteEffect ignite = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);

                collider.SetHostileEffects(fireBallDamage, DamageType.Fire, false, skillUser, null);
                explodingProjectile.explosionStatusEffects.Add(ignite);
            }
        }
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<FireBallSkillTree>().additionalManaCost;
    }
}
