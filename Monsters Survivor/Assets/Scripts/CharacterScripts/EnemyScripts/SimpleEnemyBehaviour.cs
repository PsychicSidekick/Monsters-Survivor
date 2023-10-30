using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBehaviour : MonoBehaviour
{
    SkillHandler enemySkillHandler;
    Enemy enemy;

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
        float distanceFromPlayer = Vector3.Distance(Player.instance.transform.position, transform.position);

        enemy.FacePlayer();
        enemy.FindGroundTarget();

        // if not attacking, follow target
        if (!inAttackAnimation)
        {
            enemy.Move(Player.instance.transform.position);
        }
        else
        {
            enemy.StopMoving();
        }

        // if in attack range, start attacking
        if (distanceFromPlayer <= attackRange)
        {
            enemy.animator.SetBool("isAttacking", true);
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
}
