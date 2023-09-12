using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    flat_MaxLife,
    inc_MaxLife,
    flat_LifeRegen,
    inc_LifeRegen,
    flat_MaxMana,
    inc_MaxMana,
    flat_ManaRegen,
    inc_ManaRegen,
    inc_MoveSpd,
    inc_AtkSpd,
    flat_AtkDmg,
    inc_AtkDmg,
    flat_fireRes,
    flat_coldRes,
    flat_lightningRes,
    flat_CooldownReduction,
    inc_FireDamage,
    inc_ColdDamage,
    inc_LightningDamage,
    inc_AreaDamage,
    inc_AreaEffect,
    inc_ProjDamage,
    inc_ProjSpeed,
    flat_NoOfProj,
    inc_IgniteDamage,
    inc_IgniteChance,
    inc_IgniteDuration,
    inc_FreezeChance,
    inc_FreezeDuration,
    inc_ShockEffect,
    inc_ShockChance,
    inc_ShockDuration
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
                modifierText += (value > 0 ? "+" : "-") + Mathf.Abs(value) + " to " + statType.ToString();
                break;
            case 1:
                modifierText += Mathf.Abs(value) + "%" + (value > 0 ? " increased " : " decreased ") + statType.ToString();
                break;
        }

        return modifierText;
    }
}
