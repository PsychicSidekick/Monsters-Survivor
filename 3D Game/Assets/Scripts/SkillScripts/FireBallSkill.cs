using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FireBallSkill : Skill
{
    public GameObject ballPrefab;
    public float ballRange;
    public float ballSpeed;
    public float ballBaseDmg;
    public int numberOfProjectiles;

    public float explosionRadius;
    public float explosionDamage;

    public float baseIgniteDamageMultiplier;
    public float baseIgniteDuration;
    public float baseIgniteChance;

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.animator.Play("ShootBall");
    }

    public override void UseSkill(Character skillUser)
    {
        FireBallSkillTree skillTree = skillUser.GetComponent<FireBallSkillTree>();

        float targetPosOffset = (numberOfProjectiles + skillTree.additionalFireBalls)  / -2f + 0.5f;
        for (int i = 0; i < (numberOfProjectiles + skillTree.additionalFireBalls); i++)
        {
            Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
            EffectCollider collider = Instantiate(ballPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
            Projectile proj = collider.GetComponent<Projectile>();
            ExplodingProjectile explodingProj = proj.GetComponent<ExplodingProjectile>();

            float fireBallDamage = ballBaseDmg * (1 + skillTree.increasedDamage);
            float igniteDuration = baseIgniteDuration * (1 + skillTree.increasedIgniteDuration);
            float igniteChance = baseIgniteChance + skillTree.increasedIgniteChance;
            float igniteDamageMultiplier = baseIgniteDamageMultiplier + skillTree.increasedIgniteDamageMultiplier + skillTree.increasedIgniteDamage + skillTree.increasedIgniteDuration;

            Vector3 targetPos = skillUser.GetComponent<SkillHandler>().groundTarget + skillUser.transform.right * (targetPosOffset + i);
            Vector3 direction = Vector3.Normalize(targetPos - startPos);
            proj.targetPos = startPos + direction * ballRange * (1 + skillTree.increasedRange);
            proj.projSpeed = ballSpeed * (1 + skillTree.increasedSpeed);

            explodingProj.explosionRadius = explosionRadius * (1 + skillTree.increasedExplosionRadius);
            explodingProj.explosionDamageType = DamageType.Fire;
            explodingProj.explosionDamage = explosionDamage * (1 + skillTree.increasedExplosionDamage);

            if (!skillTree.igniteAppliedByExplosion)
            {
                float igniteDamage = fireBallDamage * igniteDamageMultiplier;
                IgniteEffect ignite = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);

                collider.SetEffects(fireBallDamage, DamageType.Fire, false, skillUser, null, ignite);
            }
            else
            {
                float igniteDamage = explodingProj.explosionDamage * igniteDamageMultiplier;
                IgniteEffect ignite = new IgniteEffect(skillUser, igniteDamage, igniteDuration, igniteChance);

                collider.SetEffects(fireBallDamage, DamageType.Fire, false, skillUser, null);
                explodingProj.explosionStatusEffects.Add(ignite);
            }
          
            
        }
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<FireBallSkillTree>().additionalManaCost;
    }
}
