using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncinerateSkillTree : SkillTree
{
    public int additionalManaCost;
    public float increasedDamage;
    public float increasedRadius;
    public float increasedExtraIgniteMulti;

    public float increasedIgniteDamage;
    public bool spreadsExplosions;
    public bool doesNotRemoveIgnites;
    public bool receivesHealingFromRemovedIgnites;
    public bool doesNotDealExtraIgniteDamage;
    public bool appliesNewIgnite;

    public void IncreaseManaCost(int value)
    {
        additionalManaCost += value;
    }

    public void IncreaseDamage(float value)
    {
        increasedDamage += value;
    }

    public void IncreaseRadius(float value)
    {
        increasedRadius += value;
    }

    public void IncreaseExtraIgniteMulti(float value)
    {
        increasedExtraIgniteMulti += value;
    }

    public void IncreaseIgniteDamage(float value)
    {
        increasedIgniteDamage += value;
    }

    public void ToggleSpreadsExplosions()
    {
        spreadsExplosions = true;
    }

    public void ToggleDoesNotRemoveIgnites()
    {
        doesNotRemoveIgnites = true;
    }

    public void ToggleReceivesHealingFromRemovedIgnites()
    {
        receivesHealingFromRemovedIgnites = true;
    }

    public void ToggleDoesNotDealExtraIgniteDamage()
    {
        doesNotDealExtraIgniteDamage = true;
    }    

    public void ToggleAppliesNewIgnite()
    {
        appliesNewIgnite = true;
    }

    public override void ResetSkillTree()
    {
        additionalManaCost = 0;
        increasedDamage = 0;
        increasedRadius = 0;
        increasedExtraIgniteMulti = 0;

        increasedIgniteDamage = 0;
        spreadsExplosions = false;
        doesNotRemoveIgnites = false;
        receivesHealingFromRemovedIgnites = false;
        doesNotDealExtraIgniteDamage = false;
        appliesNewIgnite = false;
    }
}
