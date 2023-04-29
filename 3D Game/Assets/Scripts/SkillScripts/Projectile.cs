using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 targetPos;
    public float projSpeed;
    public int pierce;
    public int chain;
    public float chainingRange;
    public float chainDamageMultiplier;
    public List<Character> chainedCharacters = new List<Character>();
    public float damage;
    public DamageType dmgType;
    public Character owner;

    protected virtual void Update()
    {
        if (targetPos == null)
        {
            return;
        }

        if(Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            Destroy(gameObject);
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, projSpeed/100);
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, targetPos - transform.position, 10, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (other.GetComponent<Character>() == null)
        {
            return;
        }

        if (character.GetType() == owner.GetType())
        {
            return;
        }

        pierce--;
        chain--;
        character.ReceiveDamage(new Damage(damage, owner, dmgType));
        if (pierce < 0 && chain < 0)
        {
            Destroy(gameObject);
            return;
        }

        if (chain >= 0)
        {
            damage *= chainDamageMultiplier;
            chainedCharacters.Add(character);
            Chained();
        }
    }

    protected virtual void Chained()
    {

    }
}
