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

    public ShatterEffect(ShatterEffect shatter)
    {
        name = "shatter";
        owner = shatter.owner;
        chance = shatter.chance;
        baseDamage = shatter.baseDamage;
        shatterMultiplier = shatter.shatterMultiplier;
        doesNotRemoveFreeze = shatter.doesNotRemoveFreeze;
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
        }
    }

    public override void AddStack(Character character, StatusEffect statusEffect)
    {
        StatusEffectManager status = character.GetComponent<StatusEffectManager>();
        status.RemoveStatusEffect(this);
        status.ApplyStatusEffect(statusEffect);
    }

    public override StatusEffect CloneEffect()
    {
        return new ShatterEffect(this);
    }
}
