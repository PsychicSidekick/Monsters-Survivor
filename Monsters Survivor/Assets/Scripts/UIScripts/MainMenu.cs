using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{
    public TMP_Text longestSurvivalTimeText;
    public GameObject loadingScreen;
    public Slider loadingBar;

    public TMP_Text newGameButtonText;
    public GameObject exitButton;

    public GameObject inventoryPanel;
    public GameObject stashPanel;
    public GameObject helpPanel;
    public GameObject newGameConfirmPanel;

    private void Start()
    {
        longestSurvivalTimeText.text = "Your Longest Survival Time: " + TimeToString(PlayerPrefs.GetFloat("HighScore"));
        inventoryPanel = PlayerStorage.instance.transform.GetChild(0).gameObject;
        stashPanel = PlayerStorage.instance.transform.GetChild(1).gameObject;

        if (Application.streamingAssetsPath.Contains("://") || Application.streamingAssetsPath.Contains(":///"))
        {
            newGameButtonText.text = "Start";
            exitButton.SetActive(false);
        }
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

    public void NewGameOnClick()
    {
        if (!Application.streamingAssetsPath.Contains("://") && !Application.streamingAssetsPath.Contains(":///"))
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "/Save/save.txt");

            if (json.Length < 72 && PlayerPrefs.GetFloat("HighScore") == 0)
            {
                StartGameOnClick();
            }
            else
            {
                newGameConfirmPanel.SetActive(true);
                inventoryPanel.SetActive(false);
                stashPanel.SetActive(false);
            }
        }
        else
        {
            StartGameOnClick();
        }
    }

    public void ShowStashOnClick()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
        stashPanel.SetActive(!stashPanel.activeInHierarchy);
        helpPanel.SetActive(false);
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
        gameSave.NewSave();
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
