using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{
    public float percentOfMaximumLife;

    public override void ApplyPotionEffect()
    {
        Player.instance.GetComponent<Character>().ReceiveHealing(Player.instance.stats.maximumLife.value * percentOfMaximumLife / 100);
    }
}
