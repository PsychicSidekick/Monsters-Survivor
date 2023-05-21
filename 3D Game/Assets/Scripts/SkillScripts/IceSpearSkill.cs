using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IceSpearSkill : Skill
{
    public GameObject iceSpearPrefab;
    public float iceSpearSpeed;
    public float baseDamage;
    public int numberOfProjectiles;

    public override void OnUse(Character skillUser)
    {
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.animator.Play("ShootBall");
    }

    //public override void UseSkill(Character skillUser)
    //{
    //    Character targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;
    //    float spawnPosOffset = -numberOfProjectiles / 2f + 0.5f;
    //    for (int i = 0; i < numberOfProjectiles; i++)
    //    {
    //        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
    //        startPos += skillUser.transform.right * spawnPosOffset;
    //        spawnPosOffset++;
    //        EffectCollider collider = Instantiate(iceSpearPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
    //        collider.SetEffects(baseDamage, DamageType.Cold, false, skillUser, targetCharacter);
    //        Projectile proj = collider.GetComponent<Projectile>();
    //        proj.projSpeed = iceSpearSpeed;
    //    }
    //}

    public override void UseSkill(Character skillUser)
    {
        Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
        Vector3 targetDirection = (skillUser.GetComponent<SkillHandler>().groundTarget - startPos).normalized;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            EffectCollider collider = Instantiate(iceSpearPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
            collider.SetEffects(baseDamage, DamageType.Cold, false, skillUser, null);
            Projectile proj = collider.GetComponent<Projectile>();
            proj.targetPos = startPos + Quaternion.Euler(0, (numberOfProjectiles - 1) * -2.5f + i * 5, 0) * targetDirection * 5;
            proj.projSpeed = iceSpearSpeed;
        }
    }
}
