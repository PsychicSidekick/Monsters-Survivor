using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBeamSkillTree : SkillTree
{
    public int additionalManaCostPerSecond;

    public float increasedDamagePerSecond;
    public float increasedWidth;
    public float increasedRange;
    public float increasedDamageRampPerSecond;

    public float additionalTimeUntilOvercharged;
    public float ocIncreasedManaCostPerSecond;
    public float ocIncreasedDamagePerSecond;
    public float ocIncreasedWidth;
    public float ocIncreasedRange;

    public bool startsOvercharged;
    public bool doesNotOvercharge;

    public void IncreaseManaCostPerSecond(int value)
    {
        additionalManaCostPerSecond += value;
    }

    public void IncreaseDamagePerSecond(float value)
    {
        increasedDamagePerSecond += value;
    }

    public void IncreaseWidth(float value)
    {
        increasedWidth += value;
    }

    public void IncreaseRange(float value)
    {
        increasedRange += value;
    }

    public void IncreaseDamageRampPerSecond(float value)
    {
        increasedDamageRampPerSecond += value;
    }

    public void IncreaseTimeUntilOvercharged(float value)
    {
        additionalTimeUntilOvercharged += value;
    }

    public void IncreaseOCManaCostPerSecond(float value)
    {
        ocIncreasedManaCostPerSecond += value;
    }

    public void IncreaseOCDamagePerSecond(float value)
    {
        ocIncreasedDamagePerSecond += value;
    }

    public void IncreaseOCWidth(float value)
    {
        ocIncreasedWidth += value;
    }

    public void IncreaseOCRange(float value)
    {
        ocIncreasedRange += value;
    }

    public void ToggleStartsOvercharged()
    {
        startsOvercharged = true;
    }

    public void ToggleDoesNotOvercharge()
    {
        doesNotOvercharge = true;
    }

    public override void ResetSkillTree()
    {
        additionalManaCostPerSecond = 0;

        increasedDamagePerSecond = 0;
        increasedWidth = 0;
        increasedRange = 0;
        increasedDamageRampPerSecond = 0;

        additionalTimeUntilOvercharged = 0;
        ocIncreasedManaCostPerSecond = 0;
        ocIncreasedDamagePerSecond = 0;
        ocIncreasedWidth = 0;
        ocIncreasedRange = 0;

        startsOvercharged = false;
        doesNotOvercharge = false;
}
}
