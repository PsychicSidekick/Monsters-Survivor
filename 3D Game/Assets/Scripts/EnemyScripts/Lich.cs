using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lich : MonoBehaviour
{
    SkillHandler enemySkillHandler;
    Enemy enemy;

    public float detectionRange;
    public float attackRange;
    public bool inAttackAnimation;

    private void Start()
    {
        enemySkillHandler = GetComponent<SkillHandler>();
        enemy = GetComponent<Enemy>();
        enemySkillHandler.characterTarget = enemy.FindCharacterTarget();
    }

    private void Update()
    {
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

            // if in attack range, start attacking
            if (distanceFromPlayer <= attackRange)
            {
                enemy.animator.SetBool("isAttacking", true);
                enemy.FindGroundTarget();
                enemy.StopMoving();
                if (GetComponent<Animator>().GetFloat("ActionSpeed") != 0)
                {
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
            enemy.StopMoving();
            enemySkillHandler.skills[0].triggerSkill = false;
        }
    }
}