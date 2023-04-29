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

            Projectile proj = Instantiate(ballPrefab, startPos, Quaternion.identity).GetComponent<Projectile>();
            proj.damage += ballBaseDmg + skillUser.GetComponent<StatsManager>().attackDmg.value;
            proj.dmgType = DamageType.Fire;
            Vector3 targetPos = skillUser.GetComponent<SkillHandler>().groundTarget + skillUser.transform.right * (targetPosOffset + i);
            Vector3 direction = Vector3.Normalize(targetPos - startPos);

            proj.targetPos = startPos + direction * ballRange;
            proj.projSpeed = ballSpeed;
            proj.owner = skillUser;
        }
    }
}
