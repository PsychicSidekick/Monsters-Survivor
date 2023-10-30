using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResetSkillTreeButton : MonoBehaviour
{
    public SkillTree targetSkillTree;
    public PassivePointsCounter pointsCounter;

    public void OnClick()
    {
        targetSkillTree.ResetSkillTree();
        pointsCounter.ResetPoints();

        foreach (Transform child in transform.parent)
        {
            if (child.GetComponent<SkillPassiveButton>())
            {
                child.GetComponent<SkillPassiveButton>().ResetButton();
            }
        }
    }    
}
