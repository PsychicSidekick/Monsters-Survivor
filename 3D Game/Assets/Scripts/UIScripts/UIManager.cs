using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject skillPassivesPanel;
    public GameObject playerPassivesPanel;
    public GameObject inventoryPanel;
    public GameObject characterStatsPanel;
    public GameObject escapeMenuPanel;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleUIPanel(escapeMenuPanel);
        }
    }

    private void ToggleUIPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeInHierarchy);

        if (!skillPassivesPanel.activeInHierarchy && !playerPassivesPanel.activeInHierarchy && !inventoryPanel.activeInHierarchy && !characterStatsPanel.activeInHierarchy && !escapeMenuPanel.activeInHierarchy)
        {
            GameManager.instance.UnpauseGame();
        }
        else
        {
            GameManager.instance.PauseGame();
        }
    }

    public void ResumeOnClick()
    {
        ToggleUIPanel(escapeMenuPanel);
    }

    public void ReturnToMainMenuOnClick()
    {
        GameManager.instance.UnpauseGame();
        SceneManager.LoadScene("MainMenu");
    }
}
