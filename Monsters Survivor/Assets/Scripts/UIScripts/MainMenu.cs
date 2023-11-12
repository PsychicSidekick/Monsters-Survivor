using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject helpPanel;

    public TMP_Text longestSurvivalTimeText;
    public GameObject loadingScreen;
    public Slider loadingBar;

    public GameObject inventoryPanel;
    public GameObject stashPanel;

    private void Start()
    {
        longestSurvivalTimeText.text = "Your Longest Survival Time: " + TimeToString(PlayerPrefs.GetFloat("HighScore"));
        inventoryPanel = PlayerStorage.instance.transform.GetChild(0).gameObject;
        stashPanel = PlayerStorage.instance.transform.GetChild(1).gameObject;
    }


    // Returns a string representing the given amount of seconds in the format of "00:00"
    private string TimeToString(float time)
    {
        string minutes = Mathf.Floor(time / 60).ToString().PadLeft(2, '0');

        string seconds = Mathf.Floor(time % 60).ToString().PadLeft(2, '0');

        return minutes + ":" + seconds;
    }

    public void StartGameOnClick()
    {
        inventoryPanel.SetActive(false);
        stashPanel.SetActive(false);
        StartCoroutine(LoadSceneAsync());
    }

    public void ShowStashOnClick()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        stashPanel.SetActive(!stashPanel.activeInHierarchy);
    }

    public void ShowHelpPanelOnClick()
    {
        helpPanel.SetActive(!helpPanel.activeInHierarchy);
        inventoryPanel.SetActive(false);
        stashPanel.gameObject.SetActive(false);
    }

    public void ExitGameOnClick()
    {
        Application.Quit();
    }

    public void ClearSaveOnClick()
    {
        GameSave gameSave = new GameSave();
        gameSave.ClearSave();
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
        operation.allowSceneActivation = false;
        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            // Percentage done of the loading progress
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBar.value = progress;

            if (operation.progress == 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
