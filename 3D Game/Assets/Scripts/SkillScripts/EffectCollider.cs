using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Applies damage and status effects to colliding characters
public class EffectCollider : MonoBehaviour
{
    public bool affectsFriendlyCharacters;

    public Character owner;
    public Dictionary<Character, List<StatusEffect>> charactersStatusEffects = new Dictionary<Character, List<StatusEffect>>();

    public bool overTimeEffects;

    public float damage;
    public DamageType type;

    public List<StatusEffect> hostileInAreaStatusEffects = new List<StatusEffect>();
    public List<StatusEffect> hostileOneTimeStatusEffects = new List<StatusEffect>();

    public float healing;
    public List<StatusEffect> friendlyInAreaStatusEffects = new List<StatusEffect>();
    public List<StatusEffect> friendlyOneTimeStatusEffects = new List<StatusEffect>();

    public GameObject onHitVFX;

    public void SetHostileEffects(float _damage, DamageType _type, bool _overTimeEffects, Character _owner, StatusEffect[] _hostileInAreaStatusEffects, params StatusEffect[] _hostileOneTimeStatusEffects)
    {
        damage = _damage;
        type = _type;
        overTimeEffects = _overTimeEffects;
        if (_hostileInAreaStatusEffects != null)
        {
            hostileInAreaStatusEffects.AddRange(_hostileInAreaStatusEffects);
        }
        hostileOneTimeStatusEffects.AddRange(_hostileOneTimeStatusEffects);
        owner = _owner;
    }

    public void SetFriendlyEffects(float _healing, bool _overTimeEffects, Character _owner, StatusEffect[] _friendlyInAreaStatusEffects, params StatusEffect[] _friendlyOneTimeStatusEffects)
    {
        healing = _healing;
        overTimeEffects = _overTimeEffects;
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
        if (overTimeEffects)
        {
            foreach (Character character in charactersStatusEffects.Keys)
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
        if (!overTimeEffects)
        {
            character.ReceiveDamage(new Damage(damage, owner, type));
        }

        foreach(StatusEffect statusEffect in hostileInAreaStatusEffects)
        {
            character.status.ApplyStatusEffect(statusEffect);
            charactersStatusEffects[character].Add(statusEffect);
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
        if (!overTimeEffects)
        {
            character.ReceiveHealing(healing);
        }

        foreach (StatusEffect statusEffect in friendlyInAreaStatusEffects)
        {
            character.status.ApplyStatusEffect(statusEffect);
            charactersStatusEffects[character].Add(statusEffect);
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
        if (charactersStatusEffects.ContainsKey(character))
        {
            foreach (StatusEffect statusEffect in charactersStatusEffects[character])
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

        if (!charactersStatusEffects.ContainsKey(character))
        {
            charactersStatusEffects.Add(character, new List<StatusEffect>());
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
            charactersStatusEffects.Remove(character);
        }
    }

    private void OnDestroy()
    {
        if (charactersStatusEffects.Count == 0)
        {
            return;
        }

        if (hostileInAreaStatusEffects.Count == 0 && friendlyInAreaStatusEffects.Count == 0)
        {
            return;
        }

        foreach (Character character in charactersStatusEffects.Keys)
        {
            CleanInAreaStatusEffectsOnCharacter(character);
        }
    }
}
