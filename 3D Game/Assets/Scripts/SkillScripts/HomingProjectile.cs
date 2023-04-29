using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : Projectile
{
    public Character targetCharacter;

    protected override void Update()
    {
        if (targetCharacter != null)
        {
            targetPos = GameManager.instance.RefinedPos(targetCharacter.transform.position);
        }
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, targetPos - transform.position, 10, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookDir);
        base.Update();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>() != targetCharacter)
        {
            return;
        }
        base.OnTriggerEnter(other);
    }

    protected override void Chained()
    {
        Character nextTarget = null;
        float shortestDistance = Mathf.Infinity;
        
        Collider[] hits = Physics.OverlapSphere(targetPos, chainingRange);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Character hitCharacter = hit.GetComponent<Character>();
                if (hitCharacter == targetCharacter || chainedCharacters.Contains(hitCharacter))
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

        targetCharacter = nextTarget;
        if (nextTarget != null)
        {
            chainedCharacters.Add(targetCharacter);
        }
        base.Chained();
    }
}
