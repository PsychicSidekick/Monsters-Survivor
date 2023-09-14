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
    public TMP_Text fireResTxt;
    public TMP_Text coldResTxt;
    public TMP_Text lightningResTxt;

    public PlayerControl player;

    private void Start()
    {
        player = PlayerControl.instance;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    characterUI.SetActive(!characterUI.activeInHierarchy);
        //}

        lifeRegenTxt.text = "Life regen per second: " + player.stats.lifeRegeneration.value;
        manaRegenTxt.text = "Mana regen per second: " + player.stats.manaRegeneration.value;
        moveSpeedTxt.text = "Movement Speed: " + player.stats.movementSpeed.value;
        atkSpeedTxt.text = "Attacks per second: " + player.stats.attackSpeed.value;
        atkDamageTxt.text = "Bonus Attack Damage: " + player.stats.attackDamage.value;
        fireResTxt.text = "Fire Resistance: " + player.stats.fireResistance.value + "%";
        coldResTxt.text = "Cold Resistance: " + player.stats.coldResistance.value + "%";
        lightningResTxt.text = "Lightning Resistance: " + player.stats.lightningResistance.value + "%";
    }
}
