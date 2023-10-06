using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : Potion
{
    public float percentOfMaximumMana;

    public override void ApplyPotionEffect()
    {
        Player.instance.GetComponent<Character>().AddMana(Player.instance.stats.maximumMana.value * percentOfMaximumMana / 100);
    }
}
