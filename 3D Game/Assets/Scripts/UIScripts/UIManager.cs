using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject skillPassivesPanel;
    public GameObject inventoryPanel;
    public GameObject characterStatsPanel;
    public GameObject escapeMenuPanel;

    public Button inventoryButton;

    private void Start()
    {
        inventoryPanel = PlayerStorage.instance.transform.GetChild(0).gameObject;
        inventoryPanel.SetActive(false);
        inventoryButton.onClick.AddListener(InventoryButtonOnClick);
    }

    private void Update()
    {
        if (!Player.instance.gameObject.activeInHierarchy)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleUIPanel(skillPassivesPanel);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleUIPanel(characterStatsPanel);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryButtonOnClick();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeButtonOnClick();
        }
    }

    public void InventoryButtonOnClick()
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

    public void EscapeButtonOnClick()
    {
        if (escapeMenuPanel.activeInHierarchy)
        {
            ToggleUIPanel(escapeMenuPanel);
        }
        else if (skillPassivesPanel.activeInHierarchy)
        {
            ToggleUIPanel(skillPassivesPanel);
        }
        else if (characterStatsPanel.activeInHierarchy)
        {
            ToggleUIPanel(characterStatsPanel);
        }
        else if (inventoryPanel.activeInHierarchy)
        {
            ToggleUIPanel(inventoryPanel);
        }
        else
        {
            ToggleUIPanel(escapeMenuPanel);
        }
    }

    public void ToggleUIPanel(GameObject panel)
    {
        panel.SetActive(!panel.activeInHierarchy);

        if (!skillPassivesPanel.activeInHierarchy && !inventoryPanel.activeInHierarchy && !characterStatsPanel.activeInHierarchy && !escapeMenuPanel.activeInHierarchy)
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
