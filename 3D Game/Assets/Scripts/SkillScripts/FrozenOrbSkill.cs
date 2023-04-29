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
    public float manaCost;
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
        if (!skillUser.CheckSkillCost(manaCost))
        {
            return;
        }

        skillUser.ReduceMana(manaCost);

        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        TimedProjectile mainProj = Instantiate(frozenOrbPrefab, startPos, Quaternion.identity).GetComponent<TimedProjectile>();
        mainProj.damage += orbBaseDamage + skillUser.GetComponent<StatsManager>().attackDmg.value;
        Vector3 targetPos = skillUser.GetComponent<SkillHandler>().groundTarget;
        Vector3 direction = Vector3.Normalize(targetPos - startPos);
        mainProj.travelDirection = direction;
        mainProj.lifeTime = duration;
        mainProj.projSpeed = travelSpeed;
        mainProj.pierce = 100;
        mainProj.owner = skillUser;

        mainProj.StartCoroutine(ShootIcicles(mainProj, skillUser));
    }

    IEnumerator ShootIcicles(TimedProjectile mainProj, Character skillUser)
    {
        while (mainProj != null)
        {
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                Vector3 startPos = GameManager.instance.RefinedPos(mainProj.transform.position);
                Projectile proj = Instantiate(iciclePrefab, startPos, Quaternion.identity).GetComponent<Projectile>();
                proj.damage = icicleBaseDamage + skillUser.GetComponent<StatsManager>().attackDmg.value;
                Vector3 targetPos = (Quaternion.AngleAxis(i * 360 / numberOfProjectiles, mainProj.transform.up) * new Vector3(1, 0, 0)) + startPos;
                Vector3 direction = Vector3.Normalize(targetPos - startPos);
                proj.targetPos = startPos + direction * icicleRange;

                proj.projSpeed = projectileSpeed;
                proj.pierce = projectilePierce;
                proj.owner = skillUser;
            }

            yield return new WaitForSeconds(1 / shootRate);
        }

        yield return null;
    }
}
