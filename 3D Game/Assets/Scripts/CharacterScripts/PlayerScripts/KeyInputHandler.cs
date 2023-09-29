using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputHandler : MonoBehaviour
{
    SkillHandler playerSkillHandler;

    private void Start()
    {
        playerSkillHandler = GetComponent<SkillHandler>();
    }

    private void Update()
    {
        foreach (SkillHolder skillHolder in playerSkillHandler.skills)
        {
            if(Input.GetKey(skillHolder.key))
            {
                skillHolder.triggerSkill = true;
            }
            else
            {
                skillHolder.triggerSkill = false;
            }
        }
    }
}