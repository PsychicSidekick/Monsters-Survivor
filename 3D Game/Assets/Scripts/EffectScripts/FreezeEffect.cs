using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FreezeEffect : StatusEffect
{
    public FreezeEffect(Character owner, float duration, float chance)
    {
        name = "freeze";
        this.owner = owner;
        this.chance = chance;
        maxDuration = duration;
        remainingDuration = duration;
    }

    public FreezeEffect(FreezeEffect freeze)
    {
        name = "freeze";
        owner = freeze.owner;
        chance = freeze.chance;
        maxDuration = freeze.maxDuration;
        remainingDuration = freeze.remainingDuration;
    }

    public override void OnApply(Character character)
    {
        character.StopMoving();
        character.agent.acceleration = 0;
        character.stats.animationSpeedMultiplier = 0;
    }

    public override void OnRemove(Character character)
    {
        character.agent.acceleration = 2000;
        character.stats.animationSpeedMultiplier = 1;
    }

    public override StatusEffect CloneEffect()
    {
        return new FreezeEffect(this);
    }
}
