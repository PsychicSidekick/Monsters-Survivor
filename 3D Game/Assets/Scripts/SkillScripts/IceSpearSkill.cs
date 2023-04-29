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
        float spawnPosOffset = -numberOfProjectiles / 2f + 0.5f;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);
            startPos += skillUser.transform.right * spawnPosOffset;
            spawnPosOffset++;
            HomingProjectile proj = Instantiate(iceSpearPrefab, startPos, Quaternion.identity).GetComponent<HomingProjectile>();
            proj.targetCharacter = skillUser.GetComponent<SkillHandler>().characterTarget;
            proj.damage += baseDamage + skillUser.GetComponent<StatsManager>().attackDmg.value;
            proj.projSpeed = iceSpearSpeed;
        }
    }
}
