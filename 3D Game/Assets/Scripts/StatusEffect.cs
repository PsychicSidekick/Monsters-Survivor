using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatusEffect
{
    public string name;
    public float maxDuration;
    public float remainingDuration;
    public float chance;
    public int stacks;

    public virtual void OnApply(Character character) { }

    public virtual void AddStack(StatusEffect statusEffect) { }

    public virtual void EffectOverTime(Character character, float deltaTime) { }

    public virtual void OnRemove(Character character) { }
}