using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveDescription : MonoBehaviour
{
    void Start()
    {
        ShowDescription showDescription = GetComponent<ShowDescription>();
        SkillPassiveButton passiveSkillButton = GetComponent<SkillPassiveButton>();

        showDescription.title = passiveSkillButton.passiveName;
        if (passiveSkillButton.requiredAllocation > 0)
        {
            showDescription.description += "\n\nPrerequisite Points: " + passiveSkillButton.requiredAllocation;
        }
    }
}
