using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSkillButton : MonoBehaviour
{
    public SkillSlot currentSkillSlot;

    public Skill skill;

    public void OnClick()
    {
        transform.parent.GetComponent<ChooseSkillPanel>().chosenSkills.Remove(currentSkillSlot.chosenSkill);
        currentSkillSlot.chosenSkill = skill;
        transform.parent.GetComponent<ChooseSkillPanel>().chosenSkills.Add(skill);
        currentSkillSlot.GetComponent<Image>().sprite = skill.skillIcon;
        transform.parent.gameObject.SetActive(false);
        currentSkillSlot.SetPlayerSkill();
    }
}
