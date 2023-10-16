using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGameOnClick()
    {
        SceneManager.LoadScene("Main");
        GameObject stashPanel = PlayerStorage.instance.transform.GetChild(1).gameObject;
        stashPanel.SetActive(false);
    }

    public void ShowStashOnClick()
    {
        GameObject inventoryPanel = PlayerStorage.instance.transform.GetChild(0).gameObject;
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        GameObject stashPanel = PlayerStorage.instance.transform.GetChild(1).gameObject;
        stashPanel.SetActive(!stashPanel.activeInHierarchy);
    }

    public void ClearSaveOnClick()
    {
        GameSave gameSave = new GameSave();
        gameSave.ClearSave();
    }
}
