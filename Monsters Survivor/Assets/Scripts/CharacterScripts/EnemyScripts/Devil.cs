using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : MonoBehaviour
{
    SkillHandler enemySkillHandler;
    Enemy enemy;

    public float fireballRange;
    public float blastRange;

    public bool inAttackAnimation;
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

        // If not attacking, follow target
        if (!inAttackAnimation)
        {
            enemy.Move(Player.instance.transform.position);
        }
        else
        {
            enemy.StopMoving();
        }

        // Attack if in range
        if (distanceFromPlayer <= fireballRange)
        {
            enemy.animator.SetBool("isAttacking", true);
            enemy.FindGroundTarget();
            enemy.StopMoving();
            if (enemy.animator.GetFloat("ActionSpeed") != 0)
            {
                // Cast Fire Blast if already casted Fireball for 5 seconds
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

        // Enrage if under 30% life, increases attack speed by 50%
        if (!enraged && enemy.life <= enemy.stats.maximumLife.value * 0.3f)
        {
            enraged = true;
            enemy.stats.ApplyStatModifier(new StatModifier(StatType.AttackSpeed, 50, ModifierType.inc));
        }
    }

    // Dash towards player
    public void UseDash()
    {
        enemySkillHandler.skills[2].skill.OnUse(enemy);
    }

    public void ResetTimeSpentCastingFireBalls()
    {
        timeSpentCastingFireballs = 0;
    }
}
