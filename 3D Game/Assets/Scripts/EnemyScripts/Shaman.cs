using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : MonoBehaviour
{
    SkillHandler enemySkillHandler;
    Enemy enemy;

    public float detectionRange;
    public float meleeAttackRange;

    public bool inAttackAnimation;

    public int timesOfMeleeUsed;

    private void Start()
    {
        enemySkillHandler = GetComponent<SkillHandler>();
        enemy = GetComponent<Enemy>();
        enemySkillHandler.characterTarget = enemy.FindCharacterTarget();
    }

    private void Update()
    {
        enemy.FindGroundTarget();
        float distanceFromPlayer = Vector3.Distance(PlayerControl.instance.transform.position, transform.position);
        if (distanceFromPlayer < detectionRange)
        {
            enemy.FacePlayer();
            // if not attacking, follow target
            if (!inAttackAnimation)
            {
                enemy.Move(PlayerControl.instance.transform.position);
            }
            else
            {
                enemy.StopMoving();
            }

            if (timesOfMeleeUsed < 4)
            {
                if (distanceFromPlayer <= meleeAttackRange)
                {
                    enemy.animator.SetBool("isAttacking", true);

                    if (GetComponent<Animator>().GetFloat("ActionSpeed") != 0)
                    {
                        enemy.StopMoving();
                        enemySkillHandler.skills[0].triggerSkill = true;
                    }
                }
                else
                {
                    enemy.animator.SetBool("isAttacking", false);
                    enemySkillHandler.skills[0].triggerSkill = false;
                }
            }
            else
            {
                enemySkillHandler.skills[0].triggerSkill = false;
                enemy.animator.SetBool("isAttacking", true);
                if (GetComponent<Animator>().GetFloat("ActionSpeed") != 0)
                {
                    enemy.StopMoving();
                    enemySkillHandler.skills[1].triggerSkill = true;
                }
            }
        }
        else
        {
            enemy.StopMoving();
            enemySkillHandler.skills[0].triggerSkill = false;
            enemySkillHandler.skills[1].triggerSkill = false;
        }
    }

    public void IncrementTimesOfMeleeUsed()
    {
        timesOfMeleeUsed++;
    }

    public void ResetTimesOfMeleeUsed()
    {
        timesOfMeleeUsed = 0;
        enemySkillHandler.skills[1].triggerSkill = false;
    }
}
