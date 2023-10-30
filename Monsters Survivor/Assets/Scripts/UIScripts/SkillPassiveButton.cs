using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPassiveButton : MonoBehaviour
{
    private Button button;
    [HideInInspector] public TMP_Text textName;
    [HideInInspector] public TMP_Text allocation;

    [HideInInspector] public int timesAllocated = 0;
    public int maxAllocation;

    public SkillPassiveButton preRequisite;

    public PassivePointsCounter pointsCounter;

    private void Start()
    {
        button = GetComponent<Button>();
        GetComponent<ShowDescription>().title = gameObject.name;
        textName.text = gameObject.name;
        UpdateGUI();
    }

    private void Update()
    {
        if (pointsCounter.availablePoints <= 0)
        {
            button.interactable = false;
            return;
        }

        if (preRequisite == null)
        {
            if (timesAllocated < maxAllocation)
            {
                button.interactable = true;
            }
        }
        else
        {
            if (preRequisite.timesAllocated == preRequisite.maxAllocation && timesAllocated < maxAllocation)
            {
                button.interactable = true;
            }
            else
            {
                button.interactable = false;
            }
        }
    }

    public void UpdateGUI()
    {
        allocation.text = timesAllocated + "/" + maxAllocation;
    }

    public void ResetButton()
    {
        timesAllocated = 0;
        UpdateGUI();
    }

    public void OnClick()
    {
        if (pointsCounter.availablePoints <= 0)
        {
            return;
        }

        if (timesAllocated < maxAllocation)
        {
            pointsCounter.AddAvailablePoints(-1);
            pointsCounter.pointsSpent += 1;
            timesAllocated++;

            if (timesAllocated == maxAllocation)
            {
                button.interactable = false;
            }
        }

        UpdateGUI();
    }
}
