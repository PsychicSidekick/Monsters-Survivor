using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text longestSurvivalTimeText;

    private void Start()
    {
        longestSurvivalTimeText.text = "Your Longest Survival Time: " + TimeToString(PlayerPrefs.GetFloat("HighScore"));
    }

    private string TimeToString(float time)
    {
        string minutes = Mathf.Floor(time / 60).ToString().PadLeft(2, '0');

        string seconds = Mathf.Floor(time % 60).ToString().PadLeft(2, '0');

        return minutes + ":" + seconds;
    }

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
