using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeButton : MonoBehaviour
{
    public GameObject skillTreePanel;

    public void OnClick()
    {
        foreach(Transform child in transform.parent)
        {
            if (!child.GetComponent<Button>() && !child.GetComponent<TMP_Text>())
            {
                child.gameObject.SetActive(false);
            }
        }

        skillTreePanel.SetActive(true);
    }
}
