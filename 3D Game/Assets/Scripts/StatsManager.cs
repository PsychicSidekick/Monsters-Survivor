using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public CharacterStat fireRes;
    public CharacterStat coldRes;
    public CharacterStat lightningRes;

    [HideInInspector] public List<CharacterStat> stats = new List<CharacterStat>();

    [HideInInspector] public float animationSpeedMultiplier;

    private void Awake()
    {
        animationSpeedMultiplier = 1;
        c = GetComponent<Character>();

        stats.Add(maxLife);
        stats.Add(lifeRegen);
        stats.Add(maxMana);
        stats.Add(manaRegen);
        stats.Add(moveSpeed);
        stats.Add(attackSpeed);
        stats.Add(attackDmg);
        stats.Add(fireRes);
        stats.Add(coldRes);
        stats.Add(lightningRes);
    }

    private void Update()
    {
        c.agent.speed = animationSpeedMultiplier * moveSpeed.value;
        c.animator.SetFloat("AttackSpeed", animationSpeedMultiplier * attackSpeed.value);
        c.animator.SetFloat("ActionSpeed", animationSpeedMultiplier * moveSpeed.value);
    }

    public IEnumerator ApplyTemporaryBuff(StatModifier mod, float duration)
    {
        ApplyStatModifier(mod);
        yield return new WaitForSeconds(duration);
        RemoveStatModifier(mod);
    }

    public void ApplyStatModifier(StatModifier mod)
    {
        FindStatOfType(mod.statType).AddModifier(mod);
    }

    public void ApplyStatModifiers(List<StatModifier> mods)
    {
        foreach (StatModifier mod in mods)
        {
            ApplyStatModifier(mod);
        }
    }

    public void RemoveStatModifier(StatModifier mod)
    {
        FindStatOfType(mod.statType).RemoveModifier(mod);
    }

    public void RemoveStatModifiers(List<StatModifier> mods)
    {
        foreach (StatModifier mod in mods)
        {
            RemoveStatModifier(mod);
        }
    }

    public CharacterStat FindStatOfType(StatType type)
    {
        return stats.Find(stat => stat.type == type);
    }
}
