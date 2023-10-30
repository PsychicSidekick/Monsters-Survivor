using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text longestSurvivalTimeText;
    public GameObject loadingScreen;
    public Slider loadingBar;

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
        GameObject stashPanel = PlayerStorage.instance.transform.GetChild(1).gameObject;
        stashPanel.SetActive(false);
        StartCoroutine(LoadSceneAsync());
        //SceneManager.LoadScene("Main");
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

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
        operation.allowSceneActivation = false;
        loadingScreen.SetActive(true);
        Debug.Log("Hello");
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingBar.value = progress;

            if (operation.progress == 0.9f)
            {
                operation.allowSceneActivation = true;
            }
            Debug.Log("HJI");
            yield return null;
        }
    }
}
