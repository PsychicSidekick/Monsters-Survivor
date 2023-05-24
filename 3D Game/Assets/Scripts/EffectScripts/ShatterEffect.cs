using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterEffect : StatusEffect
{
    float baseDamage;
    float shatterMultiplier;
    bool doesNotRemoveFreeze;

    public ShatterEffect(Character owner, float damage, float multiplier, float chance, bool doesNotRemoveFreeze)
    {
        name = "shatter";
        this.owner = owner;
        this.chance = chance;
        baseDamage = damage;
        shatterMultiplier = multiplier;
        this.doesNotRemoveFreeze = doesNotRemoveFreeze;
    }

    public override void OnApply(Character character)
    {
        StatusEffectManager status = character.GetComponent<StatusEffectManager>();
        FreezeEffect freeze = (FreezeEffect)status.FindStatusEffectWithName("freeze");
        if (freeze != null)
        {
            Damage damage = new Damage(baseDamage * shatterMultiplier, owner, DamageType.Cold);
            character.ReceiveDamage(damage);

            if (doesNotRemoveFreeze)
            {
                return;
            }

            status.RemoveStatusEffect(freeze);
            foreach (StatusEffect statusEffect in status.statusEffectList)
            {
                if (!status.expiredStatusEffects.Contains(statusEffect))
                {
                    Debug.Log(statusEffect.name);
                }
            }
        }
    }

    public override void AddStack(Character character, StatusEffect statusEffect)
    {
        StatusEffectManager status = character.GetComponent<StatusEffectManager>();
        status.RemoveStatusEffect(this);
        status.ApplyStatusEffect(statusEffect);
    }
}
