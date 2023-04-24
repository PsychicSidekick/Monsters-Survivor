using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FreezeBuff : StatusEffect
{
    public FreezeBuff(float duration, float chance)
    {
        name = "freeze";
        this.chance = chance;
        maxDuration = duration;
        remainingDuration = duration;
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
}
