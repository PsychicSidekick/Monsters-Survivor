using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyromancer : MonoBehaviour
{
    SkillHandler enemySkillHandler;
    Enemy enemy;

    public float detectionRange;
    public float fireballRange;
    public float blastRange;
    public bool inAttackAnimation;

    public float timeSpentCastingFireballs;
    private bool enraged = false;

    void Start()
    {
        enemySkillHandler = GetComponent<SkillHandler>();
        enemy = GetComponent<Enemy>();
        enemySkillHandler.characterTarget = enemy.FindCharacterTarget();
    }

    void Update()
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

            if (distanceFromPlayer <= fireballRange)
            {
                enemy.animator.SetBool("isAttacking", true);
                enemy.FindGroundTarget();
                enemy.StopMoving();
                if (enemy.animator.GetFloat("ActionSpeed") != 0)
                {
                    if (timeSpentCastingFireballs >= 5)
                    {
                        enemySkillHandler.skills[0].triggerSkill = false;
                        enemySkillHandler.skills[1].triggerSkill = true;
                    }
                    else
                    {
                        timeSpentCastingFireballs += Time.deltaTime;
                        enemySkillHandler.skills[1].triggerSkill = false;
                        enemySkillHandler.skills[0].triggerSkill = true;
                    }
                }
            }
            else
            {
                enemy.animator.SetBool("isAttacking", false);
                enemySkillHandler.skills[0].triggerSkill = false;
                enemySkillHandler.skills[1].triggerSkill = false;
            }
        }
        else
        {
            enemy.StopMoving();
            enemySkillHandler.skills[0].triggerSkill = false;
        }

        if (!enraged && enemy.life <= enemy.stats.maxLife.value * 0.3f)
        {
            enraged = true;
            enemy.stats.ApplyStatModifier(new StatModifier(StatType.AtkSpd, 50, ModType.inc));
        }
    }

    public void UseDash()
    {
        enemySkillHandler.skills[2].skill.OnUse(enemy);
    }

    public void ResetTimeSpentCastingFireBalls()
    {
        timeSpentCastingFireballs = 0;
    }
}
