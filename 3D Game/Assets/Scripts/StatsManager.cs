using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    private Character c;

    public float baseMaxLife;
    public float baseLifeRegen;
    public float baseMaxMana;
    public float baseManaRegen;
    public float baseMoveSpeed;

    public CharacterStat maxLife;
    public CharacterStat lifeRegen;
    public CharacterStat maxMana;
    public CharacterStat manaRegen;
    public CharacterStat moveSpeed;

    public List<CharacterStat> stats = new List<CharacterStat>();

    private void Start()
    {
        c = GetComponent<Character>();

        maxLife = new CharacterStat(StatType.MaxLife, baseMaxLife);
        lifeRegen = new CharacterStat(StatType.LifeRegen, baseLifeRegen);
        maxMana = new CharacterStat(StatType.MaxMana, baseMaxMana);
        manaRegen = new CharacterStat(StatType.ManaRegen, baseManaRegen);
        moveSpeed = new CharacterStat(StatType.MoveSpd, baseMoveSpeed);

        stats.Add(maxLife);
        stats.Add(lifeRegen);
        stats.Add(maxMana);
        stats.Add(manaRegen);
        stats.Add(moveSpeed);
    }

    private void Update()
    {
        c.agent.speed = moveSpeed.value;
    }

    public CharacterStat FindStatOfType(StatType type)
    {
        return stats.Find(stat => stat.type == type);
    }
}
