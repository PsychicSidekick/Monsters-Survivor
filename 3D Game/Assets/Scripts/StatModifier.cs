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

    public StatModifier(StatType _statType, float _value, ModType _type)
    {
        statType = _statType;
        value = _value;
        type = _type;
    }
}
