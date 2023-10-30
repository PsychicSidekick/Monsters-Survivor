using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    MaximumLife,
    LifeRegeneration,
    MaximumMana,
    ManaRegeneration,
    MovementSpeed,
    AttackSpeed,
    AttackDamage,
    FireResistance,
    ColdResistance,
    LightningResistance,
    IncreasedFireDamage,
    IncreasedColdDamage,
    IncreasedLightningDamage,
    IncreasedAreaDamage,
    IncreasedAreaEffect,
    IncreasedProjectileDamage,
    IncreasedProjectileSpeed,
    AdditionalNumberOfProjectiles,
    IncreasedIgniteDamage,
    AdditionalIgniteChance,
    IncreasedIgniteDuration,
    IncreasedSlowEffect,
    AdditionalSlowChance,
    IncreasedSlowDuration,
    IncreasedShockEffect,
    AdditionalShockChance,
    IncreasedShockDuration
}

[Serializable]
public class CharacterStat
{
    public StatType type;
    public float baseValue;

    private bool isDirty = true;
    private float _value;

    public float value 
    {
        get 
        { 
            if (isDirty)
            {
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value; 
        } 
    }

    private readonly List<StatModifier> flatModifiers;
    private readonly List<StatModifier> incModifiers;

    public CharacterStat()
    {
        flatModifiers = new List<StatModifier>();
        incModifiers = new List<StatModifier>();
    }

    public CharacterStat(StatType _type, float _baseValue)
    {
        type = _type;
        baseValue = _baseValue;
        flatModifiers = new List<StatModifier>();
        incModifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        switch((int)mod.type)
        {
            case 0:
                flatModifiers.Add(mod);
                break;
            case 1:
                incModifiers.Add(mod);
                break;
        }
    }

    public bool RemoveModifier(StatModifier mod)
    {
        isDirty = true;
        switch ((int)mod.type)
        {
            case 0:
                return flatModifiers.Remove(mod);
            case 1:
                return incModifiers.Remove(mod);
        }

        return false;
    }

    private float CalculateFinalValue()
    {
        float finalValue = baseValue;

        foreach (StatModifier mod in flatModifiers)
        {
            finalValue += mod.value;
        }

        float totalPercentIncrease = 0;

        foreach (StatModifier mod in incModifiers)
        {
            totalPercentIncrease += mod.value / 100;
        }

        finalValue *= 1 + totalPercentIncrease;

        float roundedFinalValue = (float)Mathf.Round(finalValue * 100f) / 100f;

        if (type.ToString().Contains("Increased"))
        {
            roundedFinalValue /= 100f;
        }

        return roundedFinalValue;
    }
}
