using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSkillPanel : MonoBehaviour
{
    public List<Skill> allSkills;
    public List<Skill> chosenSkills;
    public List<Skill> availableSkills;

    public GameObject chooseSkillButtonPrefab;

    private void Start()
    {
        InitializePanel();
        gameObject.SetActive(false);
    }

    public void InitializePanel()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        availableSkills = allSkills.Except(chosenSkills).ToList();

        float rows = Mathf.Ceil(availableSkills.Count / 4f);
        float columns = Mathf.Clamp(availableSkills.Count, 0, 4);

        GetComponent<RectTransform>().sizeDelta = new Vector2(columns * 90, rows * 90);

        int buttonCount = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                float posX = 45 + 90 * j;
                float posY = -45 - 90 * i;

                GameObject chooseSkillButton = Instantiate(chooseSkillButtonPrefab, transform);
                chooseSkillButton.GetComponent<ChooseSkillButton>().skill = availableSkills[buttonCount];
                chooseSkillButton.GetComponent<Image>().sprite = availableSkills[buttonCount].skillIcon;
                chooseSkillButton.GetComponent<RectTransform>().localPosition = new Vector2(posX, posY);
                buttonCount++;
                if (buttonCount >= availableSkills.Count)
                {
                    break;
                }
            }
        }
    }
}
