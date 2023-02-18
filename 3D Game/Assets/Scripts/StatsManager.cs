using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class StatsManager : MonoBehaviour
{
    public TMP_Text lifeTxt;
    public TMP_Text manaTxt;

    public CharacterStat maxLife;
    public CharacterStat moveSpeed;
    public CharacterStat lifeRegen;
    public CharacterStat maxMana;
    public CharacterStat manaRegen;

    public List<CharacterStat> stats = new List<CharacterStat>();

    private void Start()
    {
        stats.Add(new CharacterStat(StatType.MaxLife, 100));
        maxLife = FindStatOfType(StatType.MaxLife);
        PlayerControl.instance.maxLife = maxLife.value;

        stats.Add(new CharacterStat(StatType.MoveSpd, 3));
        moveSpeed = FindStatOfType(StatType.MoveSpd);
        PlayerControl.instance.moveSpeed = moveSpeed.value;

        stats.Add(new CharacterStat(StatType.LifeRegen, 1));
        lifeRegen = FindStatOfType(StatType.LifeRegen);
        PlayerControl.instance.lifeRegen = lifeRegen.value;

        stats.Add(new CharacterStat(StatType.MaxMana, 100));
        maxMana = FindStatOfType(StatType.MaxMana);
        PlayerControl.instance.maxMana = maxMana.value;

        stats.Add(new CharacterStat(StatType.ManaRegen, 1));
        manaRegen = FindStatOfType(StatType.ManaRegen);
        PlayerControl.instance.manaRegen = manaRegen.value;
    }

    private void Update()
    {
        lifeTxt.text = (int)PlayerControl.instance.life + "/" + maxLife.value;
        manaTxt.text = (int)PlayerControl.instance.mana + "/" + maxMana.value;

        PlayerControl.instance.agent.speed = moveSpeed.value;
    }

    public CharacterStat FindStatOfType(StatType type)
    {
        return stats.Find(stat => stat.type == type);
    }
}
