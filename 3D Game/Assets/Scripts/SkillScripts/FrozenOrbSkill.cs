using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FrozenOrbSkill : Skill
{
    public GameObject frozenOrbPrefab;
    public GameObject iciclePrefab;
    public float duration;
    public float travelSpeed;
    public float orbBaseDamage;
    public float icicleBaseDamage;
    public float icicleRange;
    public float shootRate;
    public int numberOfProjectiles;
    public float projectileSpeed;
    public int projectilePierce;

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.animator.Play("ShootBall");
    }

    public override void UseSkill(Character skillUser)
    {
        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        EffectCollider mainCollider = Instantiate(frozenOrbPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
        mainCollider.SetEffects(orbBaseDamage, DamageType.Cold, false, skillUser, null);
        Projectile mainProj = mainCollider.GetComponent<Projectile>();
        Vector3 targetPos = skillUser.GetComponent<SkillHandler>().groundTarget;
        Vector3 direction = Vector3.Normalize(targetPos - startPos);
        mainProj.GetComponent<TimedProjectile>().travelDirection = direction;
        mainProj.GetComponent<TimedProjectile>().lifeTime = duration;
        mainProj.projSpeed = travelSpeed;
        mainProj.pierce = 100;

        mainCollider.StartCoroutine(ShootIcicles(mainProj, skillUser));
    }

    IEnumerator ShootIcicles(Projectile mainProj, Character skillUser)
    {
        while (mainProj != null)
        {
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                Vector3 startPos = GameManager.instance.RefinedPos(mainProj.transform.position);
                EffectCollider collider = Instantiate(iciclePrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
                collider.SetEffects(icicleBaseDamage, DamageType.Cold, false, skillUser, null);
                Projectile proj = collider.GetComponent<Projectile>();
                Vector3 targetPos = (Quaternion.AngleAxis(i * 360 / numberOfProjectiles, mainProj.transform.up) * new Vector3(1, 0, 0)) + startPos;
                Vector3 direction = Vector3.Normalize(targetPos - startPos);
                proj.targetPos = startPos + direction * icicleRange;
                proj.projSpeed = projectileSpeed;
                proj.pierce = projectilePierce;
            }

            yield return new WaitForSeconds(1 / shootRate);
        }

        yield return null;
    }
}
