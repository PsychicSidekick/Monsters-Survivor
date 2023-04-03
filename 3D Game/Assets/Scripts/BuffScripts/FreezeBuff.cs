using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBuff : Buff
{
    public FreezeBuff(float duration)
    {
        buffName = "freeze";
        maxDuration = duration;
        remainingDuration = duration;
    }

    public override void OnApply(Character character)
    {
        character.StopMoving();
        character.agent.acceleration = 0;
        character.stats.movementAnimationSpeedMultiplier = 0;
    }

    public override void OnRemove(Character character)
    {
        character.agent.acceleration = 2000;
        character.stats.movementAnimationSpeedMultiplier = 1;
    }
}
