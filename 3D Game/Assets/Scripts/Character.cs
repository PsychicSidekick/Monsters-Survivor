using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    protected StatsManager stats;

    public float life;
    public float mana;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<StatsManager>();

        life = stats.maxLife.value;
        mana = stats.maxMana.value;
    }

    public virtual void Update()
    {
        if (agent.remainingDistance < 0.1)
        {
            animator.SetBool("isWalking", false);
        }

        ReceiveHealing(stats.lifeRegen.value * Time.deltaTime);
        AddMana(stats.manaRegen.value * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackHB"))
        {
            ReceiveDamage(10);
        }
    }

    public void ReceiveDamage(float dmg)
    {
        if (life > 0)
        {
            life -= dmg;
            CheckDeath();
        }
    }

    public void ReceiveHealing(float healing)
    {
        if (life < stats.maxLife.value)
        {
            life += healing;
            if (life > stats.maxLife.value)
            {
                life = stats.maxLife.value;
            }
        }
    }

    public void ReduceMana(float value)
    {
        if (mana > 0)
        {
            mana -= value;
            if (mana < 0)
            {
                mana = 0;
            }
        }    
    }

    public void AddMana(float value)
    {
        if (mana < stats.maxMana.value)
        {
            mana += value;
            if (mana > stats.maxMana.value)
            {
                mana = stats.maxMana.value;
            }
        }
    }

    public bool CheckSkillCost(float value)
    {
        return mana >= value;
    }

    public void CheckDeath()
    {
        if(life <= 0)
        {
            life = 0;
            OnDeath();
            Destroy(gameObject);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        animator.SetBool("isWalking", true);
        agent.SetDestination(RefinedPos(targetPosition));
    }

    public void StopMoving()
    {
        agent.SetDestination(transform.position);
    }

    public virtual void OnDeath()
    {
        Debug.Log("OnDeath() from Character");
    }

    public Vector3 RefinedPos(Vector3 position)
    {
        return new Vector3(position.x, 1, position.z);
    }
}
