using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text lifeText;
    public TMP_Text manaText;
    public TMP_Text xpText;
    public TMP_Text levelText;
    public GameObject xpBar;
    private PlayerControl player;

    float lifeBallOriginalSize;
    public Image lifeMask;
    float manaBallOriginalSize;
    public Image manaMask;

    private void Start()
    {
        player = PlayerControl.instance;
        lifeBallOriginalSize = lifeMask.rectTransform.rect.width;
        manaBallOriginalSize = manaMask.rectTransform.rect.width;
    }

    private void Update()
    {
        lifeText.text = (int)player.life + "/" + (int)player.stats.maxLife.value;
        lifeMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, lifeBallOriginalSize * player.life / (float)player.stats.maxLife.value);
        manaText.text = (int)player.mana + "/" + (int)player.stats.maxMana.value;
        manaMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, manaBallOriginalSize * player.mana / (float)player.stats.maxMana.value);

        int playerLevel = player.GetCurrentLevel();
        levelText.text = "Level: " + playerLevel;

        if (playerLevel >= 100)
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
