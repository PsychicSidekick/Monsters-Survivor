using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Destroy(gameObject);
        }
    }
}
