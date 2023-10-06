using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    SkillHandler enemySkillHandler;
    Enemy enemy;

    public float fireballRange;
    public float blastRange;

    [HideInInspector] public bool inAttackAnimation;
    [HideInInspector] public float timeSpentCastingFireballs;
    [HideInInspector] private bool enraged = false;

    void Start()
    {
        enemySkillHandler = GetComponent<SkillHandler>();
        enemy = GetComponent<Enemy>();
        enemySkillHandler.characterTarget = enemy.FindCharacterTarget();
    }

    void Update()
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

        if (!enraged && enemy.life <= enemy.stats.maximumLife.value * 0.3f)
        {
            enraged = true;
            enemy.stats.ApplyStatModifier(new StatModifier(StatType.AttackSpeed, 50, ModType.inc));
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
