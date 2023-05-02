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
        skillUser.GetComponent<SkillHandler>().FaceCharacterTarget();
        skillUser.animator.Play("ShootBall");
    }

    public override void UseSkill(Character skillUser)
    {
        Character targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;
        float spawnPosOffset = -numberOfProjectiles / 2f + 0.5f;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
            startPos += skillUser.transform.right * spawnPosOffset;
            spawnPosOffset++;
            EffectCollider collider = Instantiate(iceSpearPrefab, startPos, Quaternion.identity).GetComponent<EffectCollider>();
            collider.SetEffects(baseDamage, DamageType.Cold, false, skillUser, targetCharacter);
            HomingProjectile proj = collider.GetComponent<HomingProjectile>();
            proj.projSpeed = iceSpearSpeed;
            proj.targetCharacter = targetCharacter;
        }
    }
}
