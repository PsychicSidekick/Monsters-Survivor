using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomUI : MonoBehaviour
{
    public TMP_Text lifeText;
    public TMP_Text manaText;
    public TMP_Text xpText;
    public TMP_Text levelText;
    public GameObject xpBar;
    public PlayerControl player;

    private void Update()
    {
        lifeText.text = (int)player.life + "/" + player.stats.maxLife.value;
        manaText.text = (int)player.mana + "/" + player.stats.maxMana.value;

        int playerLevel = player.GetCurrentLevel();
        levelText.text = "Level: " + playerLevel;

        if (playerLevel >= 10)
        {
            xpText.text = "MAX LEVEL";
            xpBar.transform.localScale = new Vector3(1, 1, 1);
        }

        if (player.xpIsDirty)
        {
            int prevRequiredXp = player.GetRequiredXp(playerLevel);
            int currRequiredXp = player.GetRequiredXp(playerLevel + 1);

            int xpIntoThisLevel = player.xp - prevRequiredXp;
            int requiredXpThisLevel = currRequiredXp - prevRequiredXp;

            xpText.text = xpIntoThisLevel + "/" + requiredXpThisLevel;
            xpBar.transform.localScale = new Vector3((float)xpIntoThisLevel / (float)requiredXpThisLevel, 1, 1);

            player.xpIsDirty = false;
        }
    }
}
