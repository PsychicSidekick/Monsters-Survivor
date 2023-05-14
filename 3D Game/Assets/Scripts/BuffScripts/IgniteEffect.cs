using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteEffect : StatusEffect
{
    public float totalDamage;
    public float remainingDamage;

    public IgniteEffect(Character owner, float damage, float duration, float chance)
    {
        name = "ignite";
        this.owner = owner;
        this.chance = chance;
        maxDuration = duration;
        remainingDuration = duration;
        totalDamage = damage;
        remainingDamage = damage;
    }

    public override void EffectOverTime(Character character, float deltaTime)
    {
        float damageThisFrame = totalDamage / maxDuration * deltaTime;
        character.ReceiveDamage(new Damage(damageThisFrame, owner, DamageType.Fire));
        remainingDamage -= damageThisFrame;
    }
}
