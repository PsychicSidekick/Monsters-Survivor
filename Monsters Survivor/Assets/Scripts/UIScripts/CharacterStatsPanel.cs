using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStatsPanel : MonoBehaviour
{
    public TMP_Text lifeRegenTxt;
    public TMP_Text manaRegenTxt;
    public TMP_Text fireResTxt;
    public TMP_Text coldResTxt;
    public TMP_Text lightningResTxt;
    public TMP_Text moveSpeedTxt;
    public TMP_Text atkSpeedTxt;
    public TMP_Text atkDamageTxt;
    public TMP_Text fireDamageTxt;
    public TMP_Text coldDamageTxt;
    public TMP_Text lightningDamageTxt;
    public TMP_Text areaDamageTxt;
    public TMP_Text areaEffectTxt;
    public TMP_Text projDamageTxt;
    public TMP_Text projSpeedTxt;
    public TMP_Text noOfProjsTxt;
    public TMP_Text igniteDamageTxt;
    public TMP_Text igniteDurationTxt;
    public TMP_Text slowEffectTxt;
    public TMP_Text slowDurationTxt;
    public TMP_Text shockEffectTxt;
    public TMP_Text shockDurationTxt;

    [HideInInspector] private Player player;

    private void Start()
    {
        player = Player.instance;
    }

    private void Update()
    {
        lifeRegenTxt.text = "Life Regeneration per second: " + player.stats.lifeRegeneration.value;
        manaRegenTxt.text = "Mana Regeneration per second: " + player.stats.manaRegeneration.value;
        fireResTxt.text = "Fire Resistance: " + player.stats.fireResistance.value + "%";
        coldResTxt.text = "Cold Resistance: " + player.stats.coldResistance.value + "%";
        lightningResTxt.text = "Lightning Resistance: " + player.stats.lightningResistance.value + "%";
        moveSpeedTxt.text = "Increased Movement Speed: " + (player.stats.movementSpeed.value - player.stats.movementSpeed.baseValue) / player.stats.movementSpeed.baseValue * 100 + "%";
        atkSpeedTxt.text = "Increased Attack Speed: " + (player.stats.attackSpeed.value - player.stats.attackSpeed.baseValue) / player.stats.attackSpeed.baseValue * 100 + "%";
        atkDamageTxt.text = "Bonus Base Attack Damage: " + player.stats.attackDamage.value;
        fireDamageTxt.text = "Increased Fire Damage: " + player.stats.increasedFireDamage.value * 100 + "%";
        coldDamageTxt.text = "Increased Cold Damage: " + player.stats.increasedColdDamage.value * 100 + "%";
        lightningDamageTxt.text = "Increased Lightning Damage: " + player.stats.increasedLightningDamage.value * 100 + "%";
        areaDamageTxt.text = "Increased Area Damage: " + player.stats.increasedAreaDamage.value * 100 + "%";
        areaEffectTxt.text = "Increased Area Effect: " + player.stats.increasedAreaEffect.value * 100 + "%";
        projDamageTxt.text = "Increased Projectile Damage: " + player.stats.increasedProjectileDamage.value * 100 + "%";
        projSpeedTxt.text = "Increased Projectile Speed: " + player.stats.increasedProjectileSpeed.value * 100 + "%";
        noOfProjsTxt.text = "Additional Number Of Projectiles: " + player.stats.additionalNumberOfProjectiles.value;
        igniteDamageTxt.text = "Increased Ignite Damage: " + player.stats.increasedIgniteDamage.value * 100 + "%";
        igniteDurationTxt.text = "Increased Ignite Duration: " + player.stats.increasedIgniteDuration.value * 100 + "%";
        slowEffectTxt.text = "Increased Slow Effect: " + player.stats.increasedSlowEffect.value + "%";
        slowDurationTxt.text = "Increased Slow Duration: " + player.stats.increasedSlowDuration.value * 100 + "%";
        shockEffectTxt.text = "Increased Shock Effect: " + player.stats.increasedShockEffect.value + "%";
        shockDurationTxt.text = "Increased Shock Duration: " + player.stats.increasedShockDuration.value * 100 + "%";

    }
}
