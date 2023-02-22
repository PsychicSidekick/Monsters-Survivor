using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModType
{
    Flat,
    Inc,
    More
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
            case 2:
                modifierText += Mathf.Abs(value) + "%" + (value > 0 ? " more " : " less ") + statType.ToString();
                break;
        }

        return modifierText;
    }
}
