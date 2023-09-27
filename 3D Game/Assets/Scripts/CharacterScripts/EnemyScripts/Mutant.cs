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

        int currentSkillId;
        float currentSkillRange;

        if (timesOfMeleeUsed < 2)
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

        // if in attack range, start attacking
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
