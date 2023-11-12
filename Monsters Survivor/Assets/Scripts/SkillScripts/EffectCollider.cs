using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCollider : MonoBehaviour
{
    public Character owner;
    // Stores all effects on each affected character currently
    public Dictionary<Character, List<StatusEffect>> effectsOnCharacters = new Dictionary<Character, List<StatusEffect>>();

    // Determines whether this collider applies damage/healing over time
    public bool appliesOverTime;
    // Determines whether this collider can hit friendly targets
    public bool affectsFriendlyCharacters;

    public float damage;
    public DamageType type;
    public float healing;

    // In area effects are applied on collision and removed on exit
    public List<StatusEffect> hostileInAreaStatusEffects = new List<StatusEffect>();
    public List<StatusEffect> friendlyInAreaStatusEffects = new List<StatusEffect>();
    // One time effects are applied on collision and removed after a duration
    public List<StatusEffect> hostileOneTimeStatusEffects = new List<StatusEffect>();
    public List<StatusEffect> friendlyOneTimeStatusEffects = new List<StatusEffect>();

    public GameObject onHitVFX;
    public AudioClip onHitSFX;

    public void SetHostileEffects(float _damage, DamageType _type, bool _appliesOverTime, Character _owner, StatusEffect[] _hostileInAreaStatusEffects, params StatusEffect[] _hostileOneTimeStatusEffects)
    {
        damage = _damage;
        type = _type;
        appliesOverTime = _appliesOverTime;
        if (_hostileInAreaStatusEffects != null)
        {
            hostileInAreaStatusEffects.AddRange(_hostileInAreaStatusEffects);
        }
        hostileOneTimeStatusEffects.AddRange(_hostileOneTimeStatusEffects);
        owner = _owner;
    }

    public void SetFriendlyEffects(float _healing, bool _appliesOverTime, Character _owner, StatusEffect[] _friendlyInAreaStatusEffects, params StatusEffect[] _friendlyOneTimeStatusEffects)
    {
        healing = _healing;
        appliesOverTime = _appliesOverTime;
        if (_friendlyInAreaStatusEffects != null)
        {
            friendlyInAreaStatusEffects.AddRange(_friendlyInAreaStatusEffects);
        }
        friendlyOneTimeStatusEffects.AddRange(_friendlyOneTimeStatusEffects);
        owner = _owner;
        affectsFriendlyCharacters = true;
    }

    public void Update()
    {
        if (appliesOverTime)
        {
            foreach (Character character in effectsOnCharacters.Keys)
            {
                if (character.GetType() == owner.GetType())
                {
                    character.ReceiveHealing(healing * Time.deltaTime);
                }
                else
                {
                    character.ReceiveDamage(new Damage(damage * Time.deltaTime, owner, type));
                }
            }
        }

        if (GetComponent<Projectile>() == null)
        {
            if (!owner || !owner.gameObject.activeInHierarchy)
            {
                Destroy(gameObject);
                return;
            }
        }
    }

    public void ApplyHostileEffects(Character character)
    {
        if (!appliesOverTime)
        {
            character.ReceiveDamage(new Damage(damage, owner, type));
        }

        foreach(StatusEffect statusEffect in hostileInAreaStatusEffects)
        {
            character.status.ApplyStatusEffect(statusEffect);
            effectsOnCharacters[character].Add(statusEffect);
        }

        foreach (StatusEffect statusEffect in hostileOneTimeStatusEffects)
        {
            StatusEffect clonedEffect = statusEffect.CloneEffect();
            character.GetComponent<StatusEffectManager>().ApplyStatusEffect(clonedEffect);
        }

        if (GetComponent<Projectile>() != null)
        {
            GetComponent<Projectile>().OnHit(character);
        }
    }

    public void ApplyFriendlyEffects(Character character)
    {
        if (!appliesOverTime)
        {
            character.ReceiveHealing(healing);
        }

        foreach (StatusEffect statusEffect in friendlyInAreaStatusEffects)
        {
            character.status.ApplyStatusEffect(statusEffect);
            effectsOnCharacters[character].Add(statusEffect);
        }

        foreach (StatusEffect statusEffect in friendlyOneTimeStatusEffects)
        {
            StatusEffect clonedEffect = statusEffect.CloneEffect();
            character.GetComponent<StatusEffectManager>().ApplyStatusEffect(clonedEffect);
        }

        if (GetComponent<Projectile>() != null)
        {
            GetComponent<Projectile>().OnHit(character);
        }
    }

    private void CleanInAreaStatusEffectsOnCharacter(Character character)
    {
        if (effectsOnCharacters.ContainsKey(character))
        {
            foreach (StatusEffect statusEffect in effectsOnCharacters[character])
            {
                character.status.RemoveStatusEffect(statusEffect);
            }
        }
    }

    public void OnCollide(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character == null)
        {
            return;
        }

        if (!affectsFriendlyCharacters)
        {
            if (character.GetType() == owner.GetType())
            {
                return;
            }
        }

        Projectile proj = GetComponent<Projectile>();
        if (proj)
        {
            // Skips owner if affects friendly targets and has not chained yet
            if (character == owner && proj.chain == proj.remainingChains)
            {
                return;
            }

            if (proj.chain > 0)
            {
                if (proj.chainedCharacters.Contains(character) && !proj.chainsToSameCharacters)
                {
                    return;
                }
            }
        }

        if (onHitVFX != null)
        {
            Instantiate(onHitVFX, other.ClosestPointOnBounds(transform.position), Quaternion.identity);
        }

        if (onHitSFX != null && owner != null)
        {
            owner.audioSource.clip = onHitSFX;
            owner.audioSource.Play();
        }

        if (!effectsOnCharacters.ContainsKey(character))
        {
            effectsOnCharacters.Add(character, new List<StatusEffect>());
        }

        if (character.GetType() == owner.GetType())
        {
            ApplyFriendlyEffects(character);
        }
        else
        {
            ApplyHostileEffects(character);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        OnCollide(other);
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            CleanInAreaStatusEffectsOnCharacter(character);
            effectsOnCharacters.Remove(character);
        }
    }

    private void OnDestroy()
    {
        if (effectsOnCharacters.Count == 0)
        {
            return;
        }

        if (hostileInAreaStatusEffects.Count == 0 && friendlyInAreaStatusEffects.Count == 0)
        {
            return;
        }

        foreach (Character character in effectsOnCharacters.Keys)
        {
            CleanInAreaStatusEffectsOnCharacter(character);
        }
    }
}
