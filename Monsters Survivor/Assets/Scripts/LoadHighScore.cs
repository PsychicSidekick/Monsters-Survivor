using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHighScore : MonoBehaviour
{
    private void Awake()
    {
        if (Application.streamingAssetsPath.Contains("://") || Application.streamingAssetsPath.Contains(":///"))
        {
            PlayerPrefs.SetFloat("HighScore", 0);
        }
    }

    private void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
