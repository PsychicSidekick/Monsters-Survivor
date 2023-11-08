using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : MonoBehaviour
{
    SkillHandler enemySkillHandler;
    Enemy enemy;

    public float meleeAttackRange;
    public float frozenOrbRange;

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

        int currentSkillId;
        float currentSkillRange;

        // Switch to casting Frozen Orbs after 3 melee attacks
        if (timesOfMeleeUsed < 3)
        {
            currentSkillId = 0;
            currentSkillRange = meleeAttackRange;
        }
        else
        {
            enemySkillHandler.skills[0].triggerSkill = false;
            currentSkillId = 1;
            currentSkillRange = frozenOrbRange;
        }

        // If in attack range, start attacking
        if (distanceFromPlayer <= currentSkillRange)
        {
            enemy.animator.SetBool("isAttacking", true);

            if (GetComponent<Animator>().GetFloat("ActionSpeed") != 0)
            {
                enemy.StopMoving();
                enemySkillHandler.skills[currentSkillId].triggerSkill = true;
            }
        }
        else
        {
            enemy.animator.SetBool("isAttacking", false);
            enemySkillHandler.skills[currentSkillId].triggerSkill = false;
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
