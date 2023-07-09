using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResetSkillTreeButton : MonoBehaviour
{
    public SkillTree targetSkillTree;
    public TMP_Text pointCounter;

    public void OnClick()
    {
        targetSkillTree.ResetSkillTree();
        PlayerControl.instance.AddToAvailableSkillPoints(int.Parse(pointCounter.text));
        pointCounter.text = "0";

        foreach (Transform child in transform.parent)
        {
            if (child.GetComponent<SkillPassiveButton>())
            {
                child.GetComponent<SkillPassiveButton>().ResetButton();
            }
        }
    }    
}
