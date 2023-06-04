using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockEffect : StatusEffect
{
    StatModifier shockMod;

    public ShockEffect(float shockPercentage, float duration, float chance)
    {
        name = "shock";
        this.chance = chance;
        maxDuration = duration;
        remainingDuration = duration;
        shockMod = new StatModifier(ItemModType.flat_increasedDamageTaken, shockPercentage);
    }

    public ShockEffect(ShockEffect shock)
    {
        name = "shock";
        chance = shock.chance;
        maxDuration = shock.maxDuration;
        remainingDuration = shock.remainingDuration;
        shockMod = shock.shockMod;
    }

    public override void OnApply(Character character)
    {
        character.stats.ApplyStatModifier(shockMod);
    }

    public override void AddStack(Character character, StatusEffect statusEffect)
    {
        if (((ShockEffect)statusEffect).shockMod.value >= shockMod.value)
        {
            character.stats.RemoveStatModifier(shockMod);
            shockMod = ((ShockEffect)statusEffect).shockMod;
            remainingDuration = statusEffect.maxDuration;
            character.stats.ApplyStatModifier(shockMod);
        }
    }

    public override void OnRemove(Character character)
    {
        character.stats.RemoveStatModifier(shockMod);
    }
}
