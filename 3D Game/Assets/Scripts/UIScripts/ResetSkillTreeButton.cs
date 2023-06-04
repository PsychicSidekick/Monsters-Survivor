using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSkillTreeButton : MonoBehaviour
{
    public SkillTree targetSkillTree;

    public void OnClick()
    {
        targetSkillTree.ResetSkillTree();

        foreach (Transform child in transform.parent)
        {
            if (child.GetComponent<PassiveSkillButton>())
            {
                child.GetComponent<PassiveSkillButton>().ResetButton();
            }
        }
    }    
}
