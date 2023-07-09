using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
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
    }
    
    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(PlayerControl.instance.transform.position, transform.position);
        if (distanceFromPlayer < detectionRange)
        {
            // if not attacking, follow target
            if (!inAttackAnimation)
            {
                enemy.Move(PlayerControl.instance.transform.position);
            }
            else
            {
                enemy.FacePlayer();
                enemy.StopMoving();
            }

            // if in attack range, start attacking
            if (distanceFromPlayer <= attackRange)
            {
                if (GetComponent<Animator>().GetFloat("ActionSpeed") != 0)
                {
                    enemy.StopMoving();
                    enemy.animator.SetBool("isAttacking", true);
                }
            }
            else
            {
                enemy.animator.SetBool("isAttacking", false);
            }






            //if (distanceFromPlayer < attackRange && GetComponent<Animator>().GetFloat("ActionSpeed") != 0)
            //{
            //    enemy.StopMoving();
            //    enemy.animator.SetBool("isAttacking", true);
            //}
            //else if (!enemy.animator.GetBool("isAttacking"))
            //{
            //    enemy.Move(PlayerControl.instance.transform.position);
            //    enemy.animator.SetBool("isAttacking", false);
            //}
        }
        else
        {
            enemy.StopMoving();
            enemy.animator.SetBool("isAttacking", false);
        }
        //enemySkillHandler.skills[0].triggerSkill = true;
    }
}
