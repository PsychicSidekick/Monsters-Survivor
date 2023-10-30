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
        chillMod = new StatModifier(StatModType.inc_AttackSpeed, -chillPercentage);
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
        if (((ChillEffect)statusEffect).chillMod.value <= chillMod.value)
        {
            character.stats.RemoveStatModifier(chillMod);
            chillMod = ((ChillEffect)statusEffect).chillMod;
            remainingDuration = statusEffect.maxDuration;
            character.stats.ApplyStatModifier(chillMod);
        }
    }

    public override void OnRemove(Character character)
    {
        character.stats.RemoveStatModifier(chillMod);
    }
}
