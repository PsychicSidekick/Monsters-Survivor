using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public string buffName;
    public float maxDuration;
    public float remainingDuration;
    public int stacks;

    public virtual void OnApply(Character character) { }

    public virtual void AddStack(Buff buff) { }

    public virtual void EffectOverTime(Character character, float deltaTime) { }

    public virtual void OnRemove(Character character) { }
}
