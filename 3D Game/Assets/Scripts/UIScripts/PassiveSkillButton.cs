using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PassiveSkillButton : MonoBehaviour
{
    public string passiveName;
    private Button button;
    public TMP_Text textName;
    public TMP_Text allocation;

    [HideInInspector] public int timesAllocated = 0;
    public int maxAllocation;

    public PassiveSkillButton preRequisite;
    public int requiredAllocation;

    public TMP_Text pointCounter;

    private void Start()
    {
        button = GetComponent<Button>();
        textName.text = passiveName;
        UpdateGUI();
    }

    private void Update()
    {
        if (preRequisite == null)
        {
            if (timesAllocated < maxAllocation)
            {
                button.interactable = true;
            }
            return;
        }

        if (preRequisite.timesAllocated >= requiredAllocation && timesAllocated < maxAllocation)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
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

    public void IncrementSkillTreePointCounter()
    {
        pointCounter.text = (int.Parse(pointCounter.text) + 1).ToString();
    }

    public void OnClick()
    {
        if (timesAllocated < maxAllocation)
        {
            timesAllocated++;
            IncrementSkillTreePointCounter();

            if (timesAllocated == maxAllocation)
            {
                button.interactable = false;
            }
        }

        UpdateGUI();
    }
}
