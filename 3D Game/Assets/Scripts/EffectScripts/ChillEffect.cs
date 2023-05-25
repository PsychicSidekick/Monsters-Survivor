using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillEffect : StatusEffect
{
    StatModifier chillMod;

    public ChillEffect(float chillPercentage, float duration, float chance)
    {
        name = "chill";
        this.chance = chance;
        maxDuration = duration;
        remainingDuration = duration;
        chillMod = new StatModifier(ItemModType.inc_AtkSpd, -chillPercentage);
    }

    public ChillEffect(ChillEffect chill)
    {
        name = "chill";
        chance = chill.chance;
        maxDuration = chill.maxDuration;
        remainingDuration = chill.remainingDuration;
        chillMod = chill.chillMod;
    }

    public override void OnApply(Character character)
    {
        character.stats.ApplyStatModifier(chillMod);
    }

    public override void AddStack(Character character, StatusEffect statusEffect)
    {
        remainingDuration = statusEffect.maxDuration;
    }

    public override void EffectOverTime(Character character, float deltaTime)
    {
        character.ReceiveDamage(new Damage(10 * deltaTime, character, DamageType.Fire));
    }

    public override void OnRemove(Character character)
    {
        character.stats.RemoveStatModifier(chillMod);
        character.agent.acceleration = 2000;
        character.animator.SetFloat("ActionSpeed", 1);
    }

    public override StatusEffect CloneEffect()
    {
        return new ChillEffect(this);
    }
}
