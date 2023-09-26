using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSlot : MonoBehaviour
{
    public Skill chosenSkill;
    public int skillSlotID;
    public SkillHolder skillHolder;

    public GameObject chooseSkillPanel;
    public TMP_Text cooldownText;

    private Button button;
    private Image image;

    private void Start()
    {
        skillHolder = PlayerControl.instance.GetComponent<SkillHandler>().skills[skillSlotID];
        button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (chosenSkill != null)
        {
            if (skillHolder.cooldownTime > 0)
            {
                button.enabled = false;
                image.color = button.colors.disabledColor;
                cooldownText.text = Mathf.Ceil(skillHolder.cooldownTime).ToString();
            }
            else
            {
                button.enabled = true;
                image.color = button.colors.normalColor;
                cooldownText.text = "";
            }
        }
    }

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
        skillHolder.skill = chosenSkill;
    }
}
