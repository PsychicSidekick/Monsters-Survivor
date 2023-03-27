using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ModType
{
    flat,
    inc
}

public enum ItemModType
{
    flat_MaxLife,
    flat_LifeRegen,
    flat_MaxMana,
    flat_ManaRegen,
    inc_MoveSpd,
    inc_AtkSpd,
    flat_AtkDmg,
    inc_AtkDmg,
    flat_armour,
    inc_armour,
    flat_evasion,
    inc_evasion,
    flat_fireRes,
    flat_coldRes,
    flat_lightningRes
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

    public StatModifier(ItemModType itemModType, float _value)
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
