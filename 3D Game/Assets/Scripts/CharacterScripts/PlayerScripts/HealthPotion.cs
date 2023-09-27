using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Potion
{
    public float percentOfMaximumLife;

    public override void ApplyPotionEffect()
    {
        PlayerControl.instance.GetComponent<Character>().ReceiveHealing(PlayerControl.instance.stats.maximumLife.value * percentOfMaximumLife / 100);
    }
}
