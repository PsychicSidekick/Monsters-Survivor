using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class StatsManager : MonoBehaviour
{
    public TMP_Text lifeTxt;

    public CharacterStat maxLife;

    public List<CharacterStat> stats = new List<CharacterStat>();

    private void Start()
    {
        stats.Add(new CharacterStat(StatType.Life, 100));
        maxLife = FindStat(StatType.Life);
        PlayerControl.instance.health = (int)maxLife.value;
    }

    private void Update()
    {
        lifeTxt.text = PlayerControl.instance.health + "/" + maxLife.value;
    }

    public CharacterStat FindStat(StatType type)
    {
        return stats.Find(stat => stat.type == type);
    }
}
