using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    protected Animator animator;
    NavMeshAgent agent;

    public int health;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public virtual void Update()
    {
        if (GetComponent<NavMeshAgent>().remainingDistance < 0.1)
        {
            animator.SetBool("isWalking", false);
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
        GetComponent<NavMeshAgent>().SetDestination(RefinedPos(targetPosition));
    }

    public void StopMoving()
    {
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
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
