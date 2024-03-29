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
    public bool chainsToSameCharacters;
    public float chainingRange;
    public float chainDamageMultiplier;
    public Character chainTarget;
    public List<Character> chainedCharacters = new List<Character>();

    public EffectCollider effectCollider;

    private void Start()
    {
        remainingPierces = pierce;
        remainingChains = chain;
        chainDamageMultiplier = 1;
        effectCollider = GetComponent<EffectCollider>();
    }

    protected virtual void Update()
    {
        if (targetPos == null)
        {
            return;
        }

        // Destroy when arrived at targetPos
        if (Vector3.Distance(transform.position, targetPos) == 0)
        {
            // only if projectile does not chain
            if (remainingChains == chain || remainingChains <= 0)
            {
                Destroy(gameObject);
            }
        }

        // Becomes a homing projectile when projectile started chaining
        if (remainingChains < chain && chain > 0)
        {
            // Destroy when the chain target is no longer in the scene
            if (chainTarget == null)
            {
                Destroy(gameObject);
            }
            else
            {
                targetPos = GameManager.instance.RefinedPos(chainTarget.transform.position);
            }
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, projSpeed * Time.deltaTime * 1.5f);
        Vector3 lookDir = Vector3.RotateTowards(transform.forward, targetPos - transform.position, 10, 0.0f);
        transform.rotation = Quaternion.LookRotation(lookDir);
    }

    public void OnHit(Character character)
    {
        remainingPierces--;
        remainingChains--;
        
        // Destroy projectile if cannot pierce or chain anymore
        if (remainingPierces < 0 && remainingChains < 0)
        {
            Destroy(gameObject);
            return;
        }

        // Chain to new target
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

            // Ignore if not a character
            if (!hitCharacter)
            {
                continue;
            }

            // Ignore if projecile does not chain to user and the hit character is friendly
            if (!chainsToUser && hitCharacter.GetType() == effectCollider.owner.GetType())
            {
                continue;
            }

            // Ignore last chained character
            if (hitCharacter == chainedCharacter)
            {
                continue;
            }

            // Ignore if hit character has already been chained
            if (chainedCharacters.Contains(hitCharacter))
            {
                continue;
            }

            // Update shortest distance each interation to find nearest possible chain target
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nextTarget = hitCharacter;
            }
        }

        // Set chain target if one is found
        if (nextTarget != null)
        {
            targetPos = GameManager.instance.RefinedPos(nextTarget.transform.position);
            chainTarget = nextTarget;
            EffectCollider collider = GetComponent<EffectCollider>();
            // Manually call OnTriggerEnter through OnCollide if the nextTarget is already in the collider
            // this only happens when the next target is too close to the last chained character
            if (collider.effectsOnCharacters.ContainsKey(nextTarget))
            {
                collider.effectsOnCharacters.Remove(nextTarget);
                collider.OnCollide(nextTarget.GetComponent<Collider>());
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
