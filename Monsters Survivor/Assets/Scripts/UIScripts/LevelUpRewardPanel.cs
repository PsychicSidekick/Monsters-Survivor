using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class LevelUpRewardPanel : MonoBehaviour
{
    public static System.Random random = new System.Random();

    public List<Button> rewardButtons;
    public List<StatModifier> rewards;

    public List<StatMod> possibleRewards;

    public void RandomizeRewards()
    {
        List<StatMod> statMods = possibleRewards.OrderBy(reward => random.Next()).Take(rewardButtons.Count).ToList();

        rewards = new List<StatModifier>();

        float statModifierMultiplier = 1 + Mathf.Floor(GameManager.instance.GetCurrentGameTime() / 600);

        foreach (StatMod statMod in statMods)
        {
            float modValue = statMod.value;
            if (statMod.statModType != StatModType.flat_AdditionalNumberOfProjectiles)
            {
                modValue *= statModifierMultiplier;
            }
            StatModifier statModifier = new StatModifier(statMod.statModType, modValue);
            rewards.Add(statModifier);
        }

        for (int i = 0; i < rewardButtons.Count; i++)
        {
            rewardButtons[i].transform.GetChild(0).GetComponent<TMP_Text>().text = rewards[i].modString;
        }
    }

    public void ShowRewards()
    {
        transform.parent.GetComponent<UIManager>().ToggleUIPanel(gameObject);
    }

    public void RewardButtonOnClick(Button button)
    {
        Player.instance.stats.ApplyStatModifier(rewards[rewardButtons.IndexOf(button)]);
        transform.parent.GetComponent<UIManager>().ToggleUIPanel(gameObject);
    }
}
