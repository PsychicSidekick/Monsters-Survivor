using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    public float life;
    public float maxLife;
    public float lifeRegen;
    public float mana;
    public float maxMana;
    public float manaRegen;
    public float moveSpeed;

    private void Start()
    {
        life = maxLife;
        mana = maxMana;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void Update()
    {
        if (agent.remainingDistance < 0.1)
        {
            animator.SetBool("isWalking", false);
        }

        ReceiveHealing(lifeRegen * Time.deltaTime);
        AddMana(manaRegen * Time.deltaTime);
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
        if (life < maxLife)
        {
            life += healing;
            if (life > maxLife)
            {
                life = maxLife;
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
        if (mana < maxMana)
        {
            mana += value;
            if (mana > maxMana)
            {
                mana = maxMana;
            }
        }
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
