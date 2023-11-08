using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaman : MonoBehaviour
{
    SkillHandler enemySkillHandler;
    Enemy enemy;

    public float meleeAttackRange;

    [HideInInspector] public bool inAttackAnimation;
    public int timesOfMeleeUsed;

    public GameObject meleeVFX1;
    public GameObject meleeVFX2;

    private void Start()
    {
        enemySkillHandler = GetComponent<SkillHandler>();
        enemy = GetComponent<Enemy>();
        enemySkillHandler.characterTarget = enemy.FindCharacterTarget();
    }

    private void Update()
    {
        float distanceFromPlayer = Vector3.Distance(Player.instance.transform.position, transform.position);

        enemy.FindGroundTarget();
        enemy.FacePlayer();

        // If not attacking, follow target
        if (!inAttackAnimation)
        {
            enemy.Move(Player.instance.transform.position);
        }
        else
        {
            enemy.StopMoving();
        }

        // Casts lightning orbs after 8 melee attacks
        if (timesOfMeleeUsed < 8)
        {
            // Attack if in range
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

        // Use alternating melee skill VFX to match melee attack animation
        if (timesOfMeleeUsed % 2 == 0)
        {
            GetComponent<MeleeSkillTree>().meleeVFX = meleeVFX1;
        }
        else
        {
            GetComponent<MeleeSkillTree>().meleeVFX = meleeVFX2;
        }
    }

    public void ResetTimesOfMeleeUsed()
    {
        timesOfMeleeUsed = 0;
        enemySkillHandler.skills[1].triggerSkill = false;
    }
}
