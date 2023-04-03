using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShootBallSkill : Skill
{
    public GameObject ballPrefab;
    public float ballRange;
    public float ballSpeed;
    public float shootBallManaCost;
    public float ballBaseDmg;
    public int numberOfProjectiles;

    public override void OnUse(Character _skillUser)
    {
        base.OnUse(_skillUser);
        skillUser.StopMoving();
        skillUser.GetComponent<SkillHandler>().FaceGroundTarget();
        skillUser.animator.Play("ShootBall");
    }

    public override void UseSkill()
    {
        if (!skillUser.CheckSkillCost(shootBallManaCost))
        {
            return;
        }

        skillUser.ReduceMana(shootBallManaCost);

        float targetPosOffset = numberOfProjectiles / -2f + 0.5f;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Vector3 startPos = GameManager.instance.RefinedPos(skillUser.transform.position);

            Projectile proj = Instantiate(ballPrefab, startPos, Quaternion.identity).GetComponent<Projectile>();
            proj.damage += ballBaseDmg + skillUser.GetComponent<StatsManager>().attackDmg.value;
            Vector3 targetPos = skillUser.GetComponent<SkillHandler>().groundTarget + skillUser.transform.right * (targetPosOffset + i);
            Vector3 direction = Vector3.Normalize(targetPos - startPos);

            proj.targetPos = startPos + direction * ballRange;
            proj.projSpeed = ballSpeed;
        }
    }
}
