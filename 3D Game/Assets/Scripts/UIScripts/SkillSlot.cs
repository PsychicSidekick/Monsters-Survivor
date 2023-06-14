using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlot : MonoBehaviour
{
    public Skill chosenSkill;
    public int skillSlotID;

    public GameObject chooseSkillPanel;

    public void OnClick()
    {
        if (chooseSkillPanel.activeInHierarchy)
        {
            chooseSkillPanel.SetActive(false);
            return;
        }

        chooseSkillPanel.GetComponent<ChooseSkillPanel>().InitializePanel();
        foreach (Transform child in chooseSkillPanel.transform)
        {
            child.GetComponent<ChooseSkillButton>().currentSkillSlot = this;
        }

        RectTransform chooseSkillPanelTransform = chooseSkillPanel.GetComponent<RectTransform>();
        float newXPos = transform.position.x - chooseSkillPanelTransform.rect.width / 2;
        float newYPos = transform.parent.position.y + transform.parent.GetComponent<RectTransform>().rect.height / 2 + chooseSkillPanelTransform.rect.height;
        chooseSkillPanel.transform.position = new Vector2(newXPos, newYPos);
        chooseSkillPanel.SetActive(true);
    }

    public void SetPlayerSkill()
    {
        PlayerControl.instance.GetComponent<SkillHandler>().skills[skillSlotID].skill = chosenSkill;
    }
}
