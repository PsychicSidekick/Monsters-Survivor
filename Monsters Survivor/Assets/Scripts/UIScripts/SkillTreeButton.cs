using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeButton : MonoBehaviour
{
    public GameObject skillTreePanel;
    private List<GameObject> skillTreePanels = new List<GameObject>();

    private void Start()
    {
        // Finds all other skill tree panels
        foreach (Transform child in skillTreePanel.transform.parent)
        {
            if (!child.GetComponent<Button>() && !child.GetComponent<TMP_Text>() && child.name != "ChooseTreePanel" && child.name != "Frame")
            {
                skillTreePanels.Add(child.gameObject);
            }
        }
    }

    public void OnClick()
    {
        // Disables all other skill tree panels
        foreach (GameObject skillTreePanel in skillTreePanels)
        {
            skillTreePanel.SetActive(false);
        }

        skillTreePanel.SetActive(true);
    }
}
