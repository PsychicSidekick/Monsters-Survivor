using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class StatsManager : MonoBehaviour
{
    public TMP_Text lifeTxt;

    public CharacterStat maxLife;
    public CharacterStat moveSpeed;

    public List<CharacterStat> stats = new List<CharacterStat>();

    private void Start()
    {
        stats.Add(new CharacterStat(StatType.Life, 100));
        maxLife = FindStatOfType(StatType.Life);
        PlayerControl.instance.health = (int)maxLife.value;

        stats.Add(new CharacterStat(StatType.MoveSpd, 3));
        moveSpeed = FindStatOfType(StatType.MoveSpd);
        PlayerControl.instance.moveSpeed = moveSpeed.value;
    }

    private void Update()
    {
        lifeTxt.text = PlayerControl.instance.health + "/" + maxLife.value;
        PlayerControl.instance.agent.speed = moveSpeed.value;
    }

    public CharacterStat FindStatOfType(StatType type)
    {
        return stats.Find(stat => stat.type == type);
    }
}
