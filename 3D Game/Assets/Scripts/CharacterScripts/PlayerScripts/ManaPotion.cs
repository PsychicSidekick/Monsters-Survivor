using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : Potion
{
    public float percentOfMaximumMana;

    public override void ApplyPotionEffect()
    {
        PlayerControl.instance.GetComponent<Character>().AddMana(PlayerControl.instance.stats.maximumMana.value * percentOfMaximumMana / 100);
    }
}
