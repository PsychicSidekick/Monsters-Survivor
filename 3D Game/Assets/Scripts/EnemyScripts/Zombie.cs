using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    SkillHandler enemySkillHandler;
    Enemy enemy;

    public float detectionRange;
    public float attackRange;

    private void Start()
    {
       enemySkillHandler = GetComponent<SkillHandler>();
       enemy = GetComponent<Enemy>();
    }
    
    private void Update()
    {
        //float distanceFromPlayer = Vector3.Distance(PlayerControl.instance.transform.position, transform.position);
        //if (distanceFromPlayer < detectionRange)
        //{
        //    if (distanceFromPlayer < attackRange && GetComponent<Animator>().GetFloat("ActionSpeed") != 0)
        //    {
        //        enemy.animator.SetBool("isAttacking", true);
        //    }
        //    else
        //    {
        //        enemy.Move(PlayerControl.instance.transform.position);
        //        enemy.animator.SetBool("isAttacking", false);
        //    }
        //}
        //else
        //{
        //    enemy.StopMoving();
        //    enemy.animator.SetBool("isAttacking", false);
        //}
        enemySkillHandler.skills[0].triggerSkill = true;
    }
}
