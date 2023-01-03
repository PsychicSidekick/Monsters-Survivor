using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public int health;

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
        GetComponent<NavMeshAgent>().destination = RefinedPos(targetPosition);
    }

    public void StopMoving()
    {
        GetComponent<NavMeshAgent>().destination = transform.position;
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
