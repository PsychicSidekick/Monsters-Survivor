using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockEffect : StatusEffect
{
    List<StatModifier> shockMods;

    public ShockEffect(float shockPercentage, float duration, float chance)
    {
        name = "shock";
        this.chance = chance;
        maxDuration = duration;
        remainingDuration = duration;
        StatModifier fireResMod = new StatModifier(StatModifierType.flat_FireResistance, -shockPercentage);
        StatModifier coldResMod = new StatModifier(StatModifierType.flat_ColdResistance, -shockPercentage);
        StatModifier lightningResMod = new StatModifier(StatModifierType.flat_LightningResistance, -shockPercentage);
        shockMods = new List<StatModifier> { fireResMod, coldResMod, lightningResMod};
    }

    public ShockEffect(ShockEffect shock)
    {
        name = "shock";
        chance = shock.chance;
        maxDuration = shock.maxDuration;
        remainingDuration = shock.remainingDuration;
        shockMods = shock.shockMods;
    }

    public override void OnApply(Character character)
    {
        character.stats.ApplyStatModifiers(shockMods);
    }

    public override void AddStack(Character character, StatusEffect statusEffect)
    {
        if (((ShockEffect)statusEffect).shockMods[0].value <= shockMods[0].value)
        {
            character.stats.RemoveStatModifiers(shockMods);
            shockMods = ((ShockEffect)statusEffect).shockMods;
            remainingDuration = statusEffect.maxDuration;
            character.stats.ApplyStatModifiers(shockMods);
        }
    }

    public override void OnRemove(Character character)
    {
        character.stats.RemoveStatModifiers(shockMods);
    }
}
