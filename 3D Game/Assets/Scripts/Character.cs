using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;

    public int health;
    public float moveSpeed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void Update()
    {
        if (agent.remainingDistance < 0.1)
        {
            animator.SetBool("isWalking", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttackHB"))
        {
            ReceiveDamage(10);
        }
    }

    public void ReceiveDamage(int dmg)
    {
        health -= dmg;
        CheckDeath();
    }

    public void CheckDeath()
    {
        if(health <= 0)
        {
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
