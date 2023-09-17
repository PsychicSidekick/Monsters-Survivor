using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector3 targetPos;
    public float projSpeed;
    public int pierce;
    public int remainingPierces;
    public int chain;
    public int remainingChains;
    public bool chainsToUser;
    public float chainingRange;
    public float chainDamageMultiplier;
    public List<Character> chainedCharacters = new List<Character>();

    public EffectCollider effectCollider;

    public Vector3 lastFramePosition;

    private void Start()
    {
        remainingPierces = pierce;
        remainingChains = chain;
        effectCollider = GetComponent<EffectCollider>();
    }

    protected virtual void Update()
    {
        if (targetPos == null)
        {
            return;
        }

        // destroy if arrived at targetPos
        if (Vector3.Distance(transform.position, targetPos) == 0)
        {
            Destroy(gameObject);
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, projSpeed * Time.deltaTime * 1.5f);
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, targetPos - transform.position, 10, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookDir);

        lastFramePosition = transform.position;
    }

    public void OnHit(Character character)
    {
        remainingPierces--;
        remainingChains--;
        
        // destroy projectile if cannot pierce or chain anymore
        if (remainingPierces < 0 && remainingChains < 0)
        {
            Destroy(gameObject);
            return;
        }

        // chain to new target
        if (remainingChains >= 0)
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

        Collider[] hits = Physics.OverlapSphere(chainedCharacter.transform.position, chainingRange);

        foreach (Collider hit in hits)
        {
            Character hitCharacter = hit.GetComponent<Character>();

            // if hit is not a character
            if (!hitCharacter)
            {
                continue;
            }

            // if projecile does not chain to user and the hit character is friendly
            if (!chainsToUser && hitCharacter.GetType() == effectCollider.owner.GetType())
            {
                continue;
            }

            // if hit character has already been chained
            if (hitCharacter == chainedCharacter || chainedCharacters.Contains(hitCharacter))
            {
                continue;
            }

            // update shortest distance each interation to find nearest possible chain target
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nextTarget = hitCharacter;
            }
        }

        // set chain target if one is found
        if (nextTarget != null)
        {
            targetPos = GameManager.instance.RefinedPos(nextTarget.transform.position);
            chainedCharacters.Add(nextTarget);
            //SendMessage("ChainsTo", nextTarget);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
