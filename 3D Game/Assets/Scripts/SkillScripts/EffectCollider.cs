using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Applies damage and status effects to colliding characters
public class EffectCollider : MonoBehaviour
{
    public bool affectsOwner;

    public Character owner;
    public Character target;
    public List<Character> charactersInArea;

    public float damage;
    public DamageType type;
    public bool damageOverTime;
    public bool targetsCharacter;
    public List<StatusEffect> hostileStatusEffects = new List<StatusEffect>();

    public float healing;
    public bool healingOverTime;
    public List<StatusEffect> friendlyStatusEffects = new List<StatusEffect>();

    public void SetHostileEffects(float _damage, DamageType _type, bool _damageOverTime, Character _owner, Character _target, params StatusEffect[] _hostileStatusEffects)
    {
        damage = _damage;
        type = _type;
        damageOverTime = _damageOverTime;
        hostileStatusEffects.AddRange(_hostileStatusEffects);
        owner = _owner;
        target = _target;
        if (target == null)
        {
            targetsCharacter = false;
        }
        else
        {
            targetsCharacter = true;
        }
    }

    public void SetFriendlyEffects(float _healing, bool _healingOverTime, Character _owner, params StatusEffect[] _friendlyStatusEffects)
    {
        healing = _healing;
        healingOverTime = _healingOverTime;
        friendlyStatusEffects.AddRange(_friendlyStatusEffects);
        owner = _owner;
    }

    public void Update()
    {
        if (damageOverTime)
        {
            foreach (Character character in charactersInArea)
            {
                character.ReceiveDamage(new Damage(damage * Time.deltaTime, owner, type));
            }
        }

        if (GetComponent<Projectile>() == null)
        {
            if (!owner.gameObject.activeInHierarchy)
            {
                Destroy(gameObject);
            }
        }
    }

    public void ApplyHostileEffects(Character character)
    {
        if (!damageOverTime)
        {
            character.ReceiveDamage(new Damage(damage, owner, type));
        }

        foreach (StatusEffect statusEffect in hostileStatusEffects)
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
        if (!healingOverTime)
        {
            character.ReceiveHealing(healing);
        }

        foreach (StatusEffect statusEffect in friendlyStatusEffects)
        {
            StatusEffect clonedEffect = statusEffect.CloneEffect();
            character.GetComponent<StatusEffectManager>().ApplyStatusEffect(clonedEffect);
        }

        if (GetComponent<Projectile>() != null)
        {
            GetComponent<Projectile>().OnHit(character);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();

        if (character == null)
        {
            //Debug.Log("Not character");
            return;
        }

        if (!affectsOwner)
        {
            if (character.GetType() == owner.GetType())
            {
                //Debug.Log("Not enemy");
                return;
            }
        }
        
        charactersInArea.Add(character);

        if (targetsCharacter && character != target)
        {
            //Debug.Log("Not target");
            return;
        }

        if (character.GetType() == owner.GetType())
        {
            Debug.Log("HI");
            ApplyFriendlyEffects(character);
        }
        else
        {
            ApplyHostileEffects(character);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character character = other.GetComponent<Character>();
        if (character != null)
        {
            charactersInArea.Remove(character);
        }
    }
}
