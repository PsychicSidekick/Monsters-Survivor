using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : StatusEffect
{ 
    StatModifier slowMod;
    
    public SlowEffect(float slowPercentage, float duration, float chance)
    {
        name = "slow";
        this.chance = chance;
        maxDuration = duration;
        remainingDuration = duration;
        slowMod = new StatModifier(ItemModType.inc_MoveSpd, -slowPercentage);
    }

    public SlowEffect(SlowEffect slow)
    {
        name = "slow";
        chance = slow.chance;
        maxDuration = slow.maxDuration;
        remainingDuration = slow.remainingDuration;
        slowMod = slow.slowMod;
    }

    public override void OnApply(Character character)
    {
        character.stats.ApplyStatModifier(slowMod);
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
        character.stats.RemoveStatModifier(slowMod);
        character.agent.acceleration = 2000;
        character.animator.SetFloat("ActionSpeed", 1);
    }

    public override StatusEffect CloneEffect()
    {
        return new SlowEffect(this);
    }
}
