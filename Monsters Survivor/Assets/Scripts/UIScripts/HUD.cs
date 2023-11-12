using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    public TMP_Text lifeText;
    public TMP_Text manaText;
    public TMP_Text xpText;
    public TMP_Text levelText;
    public GameObject xpBar;
    private Player player;

    float lifeBallOriginalSize;
    public Image lifeMask;
    float manaBallOriginalSize;
    public Image manaMask;

    public TMP_Text timerText;
    public bool timerStopped;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = Player.instance;
        lifeBallOriginalSize = lifeMask.rectTransform.rect.width;
        manaBallOriginalSize = manaMask.rectTransform.rect.width;
    }

    private void Update()
    {
        if (!timerStopped)
        {
            timerText.text = GameManager.instance.TimeToString(GameManager.instance.GetCurrentRunTime());
        }

        lifeText.text = (int)player.life + "/" + (int)player.stats.maximumLife.value;
        lifeMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, lifeBallOriginalSize * player.life / (float)player.stats.maximumLife.value);
        manaText.text = (int)player.mana + "/" + (int)player.stats.maximumMana.value;
        manaMask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, manaBallOriginalSize * player.mana / (float)player.stats.maximumMana.value);

        int playerLevel = player.GetCurrentLevel();
        levelText.text = "Level: " + playerLevel;

        if (player.xpIsDirty)
        {
            int prevRequiredXp = player.GetRequiredXp(playerLevel);
            int currRequiredXp = player.GetRequiredXp(playerLevel + 1);

            int xpIntoThisLevel = player.xp - prevRequiredXp;
            int requiredXpThisLevel = currRequiredXp - prevRequiredXp;

            xpText.text = xpIntoThisLevel + "/" + requiredXpThisLevel;
            xpBar.transform.localScale = new Vector3(xpIntoThisLevel / (float)requiredXpThisLevel, 1, 1);

            player.xpIsDirty = false;
        }
    }
}
