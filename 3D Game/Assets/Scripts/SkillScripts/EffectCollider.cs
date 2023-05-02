using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Applies damage and status effects to colliding characters
public class EffectCollider : MonoBehaviour
{
    public float damage;
    public DamageType type;
    public bool damageOverTime;
    public bool targetsCharacter;
    public List<StatusEffect> statusEffects = new List<StatusEffect>();
    public Character owner;
    public Character target;
    public List<Character> charactersInArea;

    public void SetEffects(float _damage, DamageType _type, bool _damageOverTime, Character _owner, Character _target, params StatusEffect[] _statusEffects)
    {
        damage = _damage;
        type = _type;
        damageOverTime = _damageOverTime;
        statusEffects.AddRange(_statusEffects);
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

    public void Update()
    {
        if (damageOverTime)
        {
            foreach (Character character in charactersInArea)
            {
                character.ReceiveDamage(new Damage(damage * Time.deltaTime, owner, type));
            }
        }
    }

    public void ApplyEffects(Character character)
    {
        if (!damageOverTime)
        {
            character.ReceiveDamage(new Damage(damage, owner, type));
        }

        foreach (StatusEffect statusEffect in statusEffects)
        {
            character.GetComponent<StatusEffectManager>().ApplyStatusEffect(statusEffect);
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

        if (character.GetType() == owner.GetType())
        {
            //Debug.Log("Not enemy");
            return;
        }

        charactersInArea.Add(character);

        if (targetsCharacter && character != target)
        {
            //Debug.Log("Not target");
            return;
        }

        ApplyEffects(character);
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
