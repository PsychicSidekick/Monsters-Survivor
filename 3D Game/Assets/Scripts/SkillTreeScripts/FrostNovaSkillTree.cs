using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostNovaSkillTree :SkillTree
{
    public int additionalManaCost;

    public float additionalCooldownTime;
    public float increasedRadius;
    public float increasedFreezeDuration;
    public float increasedSlowDuration;
    public float increasedChillDuration;
    public float increasedSlowEffect;
    public float increasedChillEffect;
    public bool slowsAndChillInstead;
    public bool doesNotStopMoving;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseCooldownTime(float value)
    {
        additionalCooldownTime += value;
    }

    public void IncreaseRadius(float value)
    {
        increasedRadius += value;
    }

    public void IncreaseFreezeDuration(float value)
    {
        increasedFreezeDuration += value;
    }

    public void IncreaseSlowDuration(float value)
    {
        increasedSlowDuration += value;
    }

    public void IncreaseChillDuration(float value)
    {
        increasedChillDuration += value;
    }

    public void IncreaseSlowEffect(float value)
    {
        increasedSlowEffect += value;
    }

    public void IncreaseChillEffect(float value)
    {
        increasedChillEffect += value;
    }

    public void ToggleSlowsAndChillInstead()
    {
        slowsAndChillInstead = true; ;
    }

    public void ToggleDoesNotStopMoving()
    {
        doesNotStopMoving = true;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;

        additionalCooldownTime = 0;
        increasedRadius = 0;
        increasedFreezeDuration = 0;
        increasedSlowDuration = 0;
        increasedChillDuration = 0;
        increasedSlowEffect = 0;
        increasedChillEffect = 0;
        slowsAndChillInstead =  false;
        doesNotStopMoving = false;
    }
}
