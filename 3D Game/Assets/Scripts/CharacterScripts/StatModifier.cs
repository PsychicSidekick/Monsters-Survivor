using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public enum ModType
{
    flat,
    inc
}

[Serializable]
public class StatMod
{
    public StatModType statModType;
    public float value;
}

public enum StatModType
{
    flat_MaximumLife,
    inc_MaximumLife,
    flat_LifeRegeneration,
    inc_MaximumMana,
    inc_ManaRegeneration,
    inc_MovementSpeed,
    inc_AttackSpeed,
    flat_AttackDamage,
    flat_FireResistance,
    flat_ColdResistance,
    flat_LightningResistance,
    flat_IncreasedFireDamage,
    flat_IncreasedColdDamage,
    flat_IncreasedLightningDamage,
    flat_IncreasedAreaDamage,
    flat_IncreasedAreaEffect,
    flat_IncreasedProjectileDamage,
    flat_IncreasedProjectileSpeed,
    flat_AdditionalNumberOfProjectiles,
    flat_IncreasedIgniteDamage,
    flat_IncreasedIgniteDuration,
    flat_IncreasedSlowEffect,
    flat_IncreasedSlowDuration,
    flat_IncreasedShockEffect,
    flat_IncreasedShockDuration
}

public class StatModifier
{
    public readonly StatType statType;
    public readonly float value;
    public readonly ModType type;
    public readonly string modString;

    public StatModifier(StatType _statType, float _value, ModType _type)
    {
        statType = _statType;
        value = _value;
        type = _type;
        modString = ToString();
    }

    public StatModifier(StatModType itemModType, float _value)
    {
        string[] modType_statType = itemModType.ToString().Split("_");
        statType = (StatType)Enum.Parse(typeof(StatType), modType_statType[1], true);
        value = _value;
        type = (ModType)Enum.Parse(typeof(ModType), modType_statType[0], true);
        modString = ToString();
    }

    public override string ToString()
    {
        string modifierText = null;

        switch((int)type)
        {
            case 0:
                if (statType.ToString().Contains("Increased"))
                {
                    modifierText += Mathf.Abs(value) + "% " + StatTypeToString(statType);
                }
                else if (statType.ToString().Contains("Additional"))
                {
                    if (statType.ToString().Contains("Chance"))
                    {
                        modifierText += Mathf.Abs(value) + "% " + StatTypeToString(statType);
                    }
                    else
                    {
                        modifierText += "+" + Mathf.Abs(value) + " " + StatTypeToString(statType);
                    }
                }
                else
                {
                    modifierText += "+" + Mathf.Abs(value) + " to " + StatTypeToString(statType);
                }
                
                break;
            case 1:
                modifierText += Mathf.Abs(value) + "% " + "increased " + StatTypeToString(statType);
                break;
        }

        return modifierText;
    }

    public string StatTypeToString(StatType statType)
    {
        var result = new StringBuilder();

        string statTypeString = statType.ToString();
        foreach (char ch in statTypeString)
        {
            if (char.IsUpper(ch) && result.Length > 0)
            {
                result.Append(' ');
            }
            result.Append(ch);
        }

        return result.ToString();
    }
}
