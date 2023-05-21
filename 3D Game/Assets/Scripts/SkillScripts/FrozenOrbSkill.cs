using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FrozenOrbSkill : Skill
{
    public GameObject frozenOrbPrefab;
    public GameObject iciclePrefab;

    public float baseOrbDuration;
    public float baseOrbTravelSpeed;
    public float baseOrbDamage;
    public float baseOrbFreezeDuration;
    public float baseOrbFreezeChance;

    public float baseIcicleDamage;
    public float baseIcicleRange;
    public float baseIcicleShootRate;
    public int baseNumberOfIcicles;
    public float baseIcicleTravelSpeed;
    public int baseIciclePierce;
    public float baseIcicleFreezeDuration;
    public float baseIcicleFreezeChance;

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.animator.Play("ShootBall");
    }

    public override void UseSkill(Character skillUser)
    {
        FrozenOrbSkillTree skillTree = skillUser.GetComponent<FrozenOrbSkillTree>();

        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        EffectCollider orbCollider = Instantiate(frozenOrbPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
        float orbDamage = baseOrbDamage * (1 + skillTree.increasedOrbDamage);
        float orbFreezeDuration = baseOrbFreezeDuration * (1 + skillTree.increasedFreezeDuration);
        float orbFreezeChance = baseOrbFreezeChance + skillTree.increasedOrbFreezeChance;
        FreezeEffect freeze = new FreezeEffect(skillUser, orbFreezeDuration, orbFreezeChance);
        orbCollider.SetEffects(orbDamage, DamageType.Cold, false, skillUser, null, freeze);

        Projectile orbProj = orbCollider.GetComponent<Projectile>();
        Vector3 targetPos = skillUser.GetComponent<SkillHandler>().groundTarget;
        Vector3 direction = Vector3.Normalize(targetPos - startPos);
        orbProj.GetComponent<TimedProjectile>().travelDirection = direction;
        orbProj.GetComponent<TimedProjectile>().lifeTime = baseOrbDuration * (1 + skillTree.increasedOrbDuration);
        orbProj.projSpeed = baseOrbTravelSpeed * (1 + skillTree.increasedOrbTravelSpeed);
        orbProj.pierce = 100;

        orbCollider.StartCoroutine(ShootIcicles(orbProj, skillUser));
    }

    IEnumerator ShootIcicles(Projectile orbProj, Character skillUser)
    {
        FrozenOrbSkillTree skillTree = skillUser.GetComponent<FrozenOrbSkillTree>();

        while (orbProj != null)
        {
            for (int i = 0; i < baseNumberOfIcicles + skillTree.additionalIcicles; i++)
            {
                Vector3 startPos = GameManager.instance.RefinedPos(orbProj.transform.position);
                EffectCollider icicleCollider = Instantiate(iciclePrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
                float icicleDamage = baseIcicleDamage * (1 + skillTree.increasedIcicleDamage);
                float icicleFreezeDuration = baseIcicleFreezeDuration * (1 + skillTree.increasedFreezeDuration);
                float icicleFreezeChance = baseIcicleFreezeChance + skillTree.increasedIcicleFreezeChance;
                FreezeEffect freeze = new FreezeEffect(skillUser, icicleFreezeDuration, icicleFreezeChance);
                icicleCollider.SetEffects(icicleDamage, DamageType.Cold, false, skillUser, null, freeze);

                Projectile icicleProj = icicleCollider.GetComponent<Projectile>();
                Vector3 targetPos = (Quaternion.AngleAxis(i * 360 / (baseNumberOfIcicles + skillTree.additionalIcicles), orbProj.transform.up) * new Vector3(1, 0, 0)) + startPos;
                Vector3 direction = Vector3.Normalize(targetPos - startPos);
                icicleProj.targetPos = startPos + direction * baseIcicleRange * (1 + skillTree.increasedIcicleRange);
                icicleProj.projSpeed = baseIcicleTravelSpeed * (1 + skillTree.increasedIcicleTravelSpeed);
                icicleProj.pierce = baseIciclePierce + skillTree.additionalIciclePierce;
            }

            yield return new WaitForSeconds(1 / (baseIcicleShootRate * (1 + skillTree.increasedIcicleShootRate)));
        }

        yield return null;
    }

    public override float GetManaCost(Character skillUser)
    {
        return baseManaCost + skillUser.GetComponent<FrozenOrbSkillTree>().additionalManaCost;
    }
}
