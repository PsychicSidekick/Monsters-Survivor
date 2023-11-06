using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : StatusEffect
{ 
    StatModifier slowMod;
    bool disabled;
    
    public SlowEffect(float slowPercentage, float duration, float chance)
    {
        name = "slow";
        this.chance = chance;
        maxDuration = duration;
        remainingDuration = duration;
        slowMod = new StatModifier(StatModifierType.inc_MovementSpeed, -slowPercentage);
        disabled = false;
    }

    public SlowEffect(SlowEffect slow)
    {
        name = "slow";
        chance = slow.chance;
        maxDuration = slow.maxDuration;
        remainingDuration = slow.remainingDuration;
        slowMod = slow.slowMod;
        disabled = true;
    }

    public override void OnApply(Character character)
    {
        character.stats.ApplyStatModifier(slowMod);
    }

    // Called to all existing slow effects on the character when a new slow effect is added
    public override void AddStack(Character character, StatusEffect newStatusEffect)
    {
        // Add and apply new slow effect if have not yet done so
        if (!character.status.statusEffectList.Contains(newStatusEffect))
        {
            character.status.statusEffectList.Add(newStatusEffect);
            newStatusEffect.OnApply(character);
        }

        SlowEffect newSlow = (SlowEffect)newStatusEffect;

        /* This slow effect is disabled if the new slow effect has the same amount of effect and more duration
        or if the new slow effect has more effect. Otherwise the new slow effect will be disabled.*/
        if (newSlow.slowMod.value == slowMod.value)
        {
            if (newSlow.remainingDuration > remainingDuration)
            {
                disabled = true;
                character.stats.RemoveStatModifier(slowMod);
            }
            else
            {
                newSlow.disabled = true;
                character.stats.RemoveStatModifier(newSlow.slowMod);
            }
        }
        else if (newSlow.slowMod.value < slowMod.value)
        {
            disabled = true;
            character.stats.RemoveStatModifier(slowMod);
        }
        else
        {
            newSlow.disabled = true;
            character.stats.RemoveStatModifier(newSlow.slowMod);
        }
    }

    public override void OnRemove(Character character)
    {
        character.stats.RemoveStatModifier(slowMod);
        // Enable the next best slow mod if this slow mod was enabled when removed
        if (!disabled && character.status.FindStatusEffectsWithName("slow").Count > 1)
        {
            SlowEffect nextBestSlowEffect = FindNextBestSlowEffect(character, this);
            if (nextBestSlowEffect.disabled == false)
            {
                return;
            }
            nextBestSlowEffect.disabled = false;
            nextBestSlowEffect.OnApply(character);
        }
    }

    private SlowEffect FindNextBestSlowEffect(Character character, SlowEffect currentBestSlow)
    {
        float highestSlowValue = 0;
        SlowEffect bestSlowEffect = null;

        foreach (StatusEffect statusEffect in character.status.statusEffectList)
        {
            if (statusEffect.GetType() == typeof(SlowEffect))
            {
                if (statusEffect == currentBestSlow)
                {
                    continue;
                }
                if (((SlowEffect)statusEffect).slowMod.value < highestSlowValue)
                {
                    highestSlowValue = ((SlowEffect)statusEffect).slowMod.value;
                    bestSlowEffect = (SlowEffect)statusEffect;
                }
            }
        }

        return bestSlowEffect;
    }
}
