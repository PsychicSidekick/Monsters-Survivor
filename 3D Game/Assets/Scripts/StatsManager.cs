using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private Character c;

    public CharacterStat maximumLife;
    public CharacterStat lifeRegeneration;
    public CharacterStat maximumMana;
    public CharacterStat manaRegeneration;
    public CharacterStat movementSpeed;
    public CharacterStat attackSpeed;
    public CharacterStat attackDamage;
    public CharacterStat fireResistance;
    public CharacterStat coldResistance;
    public CharacterStat lightningResistance;
    public CharacterStat increasedFireDamage;
    public CharacterStat increasedColdDamage;
    public CharacterStat increasedLightningDamage;
    public CharacterStat increasedAreaDamage;
    public CharacterStat increasedAreaEffect;
    public CharacterStat increasedProjectileDamage;
    public CharacterStat increasedProjectileSpeed;
    public CharacterStat additionalNumberOfProjectiles;
    public CharacterStat increasedIgniteDamage;
    public CharacterStat additionalIgniteChance;
    public CharacterStat increasedIgniteDuration;
    public CharacterStat increasedSlowEffect;
    public CharacterStat additionalSlowChance;
    public CharacterStat increasedSlowDuration;
    public CharacterStat increasedShockEffect;
    public CharacterStat additionalShockChance;
    public CharacterStat increasedShockDuration;

    [HideInInspector] public List<CharacterStat> stats = new List<CharacterStat>();

    [HideInInspector] public float animationSpeedMultiplier;

    private void Awake()
    {
        animationSpeedMultiplier = 1;
        c = GetComponent<Character>();

        stats.Add(maximumLife);
        stats.Add(lifeRegeneration);
        stats.Add(maximumMana);
        stats.Add(manaRegeneration);
        stats.Add(movementSpeed);
        stats.Add(attackSpeed);
        stats.Add(attackDamage);
        stats.Add(fireResistance);
        stats.Add(coldResistance);
        stats.Add(lightningResistance);
        stats.Add(increasedFireDamage);
        stats.Add(increasedColdDamage);
        stats.Add(increasedLightningDamage);
        stats.Add(increasedAreaDamage);
        stats.Add(increasedAreaEffect);
        stats.Add(increasedProjectileDamage);
        stats.Add(increasedProjectileSpeed);
        stats.Add(additionalNumberOfProjectiles);
        stats.Add(increasedIgniteDamage);
        stats.Add(additionalIgniteChance);
        stats.Add(increasedIgniteDuration);
        stats.Add(increasedSlowEffect);
        stats.Add(additionalSlowChance);
        stats.Add(increasedSlowDuration);
        stats.Add(increasedShockEffect);
        stats.Add(additionalShockChance);
        stats.Add(increasedShockDuration);
    }

    private void Update()
    {
        c.agent.speed = animationSpeedMultiplier * movementSpeed.value;
        c.animator.SetFloat("AttackSpeed", animationSpeedMultiplier * attackSpeed.value);
        c.animator.SetFloat("ActionSpeed", animationSpeedMultiplier * movementSpeed.value);
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
