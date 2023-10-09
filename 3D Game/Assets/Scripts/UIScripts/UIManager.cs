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

    private void Start()
    {
        inventoryPanel = PlayerStorage.instance.transform.GetChild(0).gameObject;
        inventoryPanel.SetActive(false);
    }

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
            PlayerStorage.instance.descriptionPanel.SetActive(false);
            if (PlayerStorage.instance.cursorItem != null)
            {
                PlayerStorage.instance.DropItem(PlayerStorage.instance.cursorItem);
                PlayerStorage.instance.cursorItem = null;
                PlayerStorage.instance.lockCursor = false;
            }
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
        PlayerStorage.instance.transform.GetChild(0).gameObject.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
