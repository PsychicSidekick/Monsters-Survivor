using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBuff : StatusEffect
{ 
    private StatModifier slowMod;
    
    public SlowBuff(float slowPercentage, float duration)
    {
        name = "slow";
        slowMod = new StatModifier(ItemModType.inc_MoveSpd, -slowPercentage);
        maxDuration = duration;
        remainingDuration = duration;
    }

    public override void OnApply(Character character)
    {
        character.stats.ApplyStatModifier(slowMod);
    }

    public override void AddStack(StatusEffect buff)
    {
        remainingDuration = buff.maxDuration;
    }

    public override void EffectOverTime(Character character, float deltaTime)
    {
        character.ReceiveDamage(new Damage(10 * deltaTime, character, DamageType.Fire));
    }

    public override void OnRemove(Character character)
    {
        character.stats.RemoveStatModifier(slowMod);
        character.agent.acceleration = 2000;
        character.animator.SetFloat("ActionSpeed", 1);
    }
}
