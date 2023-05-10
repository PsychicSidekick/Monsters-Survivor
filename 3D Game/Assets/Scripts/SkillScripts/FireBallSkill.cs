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

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.animator.Play("ShootBall");
    }

    public override void UseSkill(Character skillUser)
    {
        float targetPosOffset = numberOfProjectiles / -2f + 0.5f;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
            EffectCollider collider = Instantiate(ballPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
            collider.SetEffects(ballBaseDmg, DamageType.Fire, false, skillUser, null);

            Projectile proj = collider.GetComponent<Projectile>();
            Vector3 targetPos = skillUser.GetComponent<SkillHandler>().groundTarget + skillUser.transform.right * (targetPosOffset + i);
            Vector3 direction = Vector3.Normalize(targetPos - startPos);
            proj.targetPos = startPos + direction * ballRange;
            proj.projSpeed = ballSpeed;

            ExplodingProjectile explodingProj = proj.GetComponent<ExplodingProjectile>();
            explodingProj.explosionRadius = explosionRadius;
            explodingProj.explosionDamageType = DamageType.Fire;
            explodingProj.explosionDamage = explosionDamage;
        }
    }
}
