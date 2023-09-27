using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject skillPassivesPanel;
    public GameObject playerPassivesPanel;
    public GameObject inventoryPanel;
    public GameObject characterStatsPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleUIPanel(skillPassivesPanel);
            playerPassivesPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleUIPanel(playerPassivesPanel);
            skillPassivesPanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleUIPanel(characterStatsPanel);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleUIPanel(inventoryPanel);
        }
    }

    private void ToggleUIPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeInHierarchy);

        if (!skillPassivesPanel.activeInHierarchy && !playerPassivesPanel.activeInHierarchy && !inventoryPanel.activeInHierarchy && !characterStatsPanel.activeInHierarchy)
        {
            GameManager.instance.UnpauseGame();
        }
        else
        {
            GameManager.instance.PauseGame();
        }
    }
}
