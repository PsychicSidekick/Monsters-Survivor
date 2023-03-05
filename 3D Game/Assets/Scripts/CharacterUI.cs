using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterUI : MonoBehaviour
{
    public GameObject characterUI;

    public TMP_Text lifeRegenTxt;
    public TMP_Text manaRegenTxt;
    public TMP_Text moveSpeedTxt;
    public TMP_Text atkSpeedTxt;
    public TMP_Text atkDamageTxt;
    public TMP_Text armourTxt;
    public TMP_Text evasionTxt;
    public TMP_Text fireResTxt;
    public TMP_Text coldResTxt;
    public TMP_Text lightningResTxt;

    public PlayerControl player;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            characterUI.SetActive(!characterUI.activeInHierarchy);
        }

        lifeRegenTxt.text = "Life regen per second: " + player.stats.lifeRegen.value;
        manaRegenTxt.text = "Mana regen per second: " + player.stats.manaRegen.value;
        moveSpeedTxt.text = "Movement Speed: " + player.stats.moveSpeed.value;
        atkSpeedTxt.text = "Attacks per second: " + player.stats.attackSpeed.value;
        atkDamageTxt.text = "Bonus Attack Damage: " + player.stats.attackDmg.value;
        armourTxt.text = "Armour: " + player.stats.armor.value;
        evasionTxt.text = "Evasion: " + player.stats.evasion.value;
        fireResTxt.text = "Fire Resistance: " + player.stats.fireRes.value + "%";
        coldResTxt.text = "Cold Resistance: " + player.stats.coldRes.value + "%";
        lightningResTxt.text = "Lightning Resistance: " + player.stats.lightningRes.value + "%";
    }
}
