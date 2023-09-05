using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : MonoBehaviour
{
    SkillHandler enemySkillHandler;
    Enemy enemy;

    public float meleeAttackRange;

    [HideInInspector] public bool inAttackAnimation;
    [HideInInspector] public int timesOfMeleeUsed;

    private void Start()
    {
        enemySkillHandler = GetComponent<SkillHandler>();
        enemy = GetComponent<Enemy>();
        enemySkillHandler.characterTarget = enemy.FindCharacterTarget();
    }

    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(PlayerControl.instance.transform.position, transform.position);

        enemy.FindGroundTarget();
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
