using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteEffect : StatusEffect
{
    float totalDamage;

    public IgniteEffect(Character owner, float damage, float duration, float chance)
    {
        name = "ignite";
        this.owner = owner;
        this.chance = chance;
        maxDuration = duration;
        remainingDuration = duration;
        totalDamage = damage;
    }

    public override void EffectOverTime(Character character, float deltaTime)
    {
        character.ReceiveDamage(new Damage(totalDamage/maxDuration * deltaTime, owner, DamageType.Fire));
    }
}
