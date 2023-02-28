using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatsManager : MonoBehaviour
{
    private Character c;

    public CharacterStat maxLife;
    public CharacterStat lifeRegen;
    public CharacterStat maxMana;
    public CharacterStat manaRegen;
    public CharacterStat moveSpeed;
    public CharacterStat attackSpeed;
    public CharacterStat attackDmg;
    public CharacterStat armor;
    public CharacterStat evasion;

    [HideInInspector]
    public List<CharacterStat> stats = new List<CharacterStat>();

    private void Awake()
    {
        c = GetComponent<Character>();

        stats.Add(maxLife);
        stats.Add(lifeRegen);
        stats.Add(maxMana);
        stats.Add(manaRegen);
        stats.Add(moveSpeed);
        stats.Add(attackSpeed);
        stats.Add(attackDmg);
        stats.Add(armor);
        stats.Add(evasion);
    }

    private void Update()
    {
        c.agent.speed = moveSpeed.value;
        c.animator.SetFloat("AttackSpeed", attackSpeed.value);
    }

    public CharacterStat FindStatOfType(StatType type)
    {
        return stats.Find(stat => stat.type == type);
    }
}
