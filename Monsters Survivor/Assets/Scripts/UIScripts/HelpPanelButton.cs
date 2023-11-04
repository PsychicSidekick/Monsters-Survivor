using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanelButton : MonoBehaviour
{
    public GameObject content;

    public void OnClick()
    {
        foreach (Transform contentPanel in content.transform.parent)
        {
            contentPanel.gameObject.SetActive(false);
        }

        content.SetActive(true);
    }
}
