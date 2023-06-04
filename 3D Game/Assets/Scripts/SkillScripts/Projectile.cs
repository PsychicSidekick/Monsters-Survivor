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

    public EffectCollider effectCollider;

    private void Start()
    {
        effectCollider = GetComponent<EffectCollider>();
    }

    protected virtual void Update()
    {
        if (targetPos == null)
        {
            return;
        }

        if (Vector3.Distance(transform.position, targetPos) == 0)
        {
            Destroy(gameObject);
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, projSpeed/100);
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, targetPos - transform.position, 10, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    public void OnHit(Character character)
    {
        pierce--;
        chain--;
        
        if (pierce < 0 && chain < 0)
        {
            Destroy(gameObject);
            return;
        }

        if (chain >= 0)
        {
            effectCollider.damage *= chainDamageMultiplier;
            chainedCharacters.Add(character);
            Chained(character);
        }
    }

    public void Chained(Character chainedCharacter)
    {
        Character nextTarget = null;
        float shortestDistance = Mathf.Infinity;

        Collider[] hits = Physics.OverlapSphere(targetPos, chainingRange);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Character hitCharacter = hit.GetComponent<Character>();
                if (hitCharacter == chainedCharacter || chainedCharacters.Contains(hitCharacter))
                {
                    continue;
                }
                float distance = Vector3.Distance(targetPos, hit.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nextTarget = hitCharacter;
                }
            }
        }

        //effectCollider.target = nextTarget;
        
        //if (effectCollider.charactersInArea.Contains(nextTarget))
        //{
        //    effectCollider.ApplyEffects(nextTarget);
        //}
        if (nextTarget != null)
        {
            targetPos = nextTarget.transform.position;
            chainedCharacters.Add(nextTarget);
            SendMessage("ChainsTo", nextTarget);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
