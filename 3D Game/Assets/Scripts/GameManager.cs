using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        PlayerControl.instance.xp = PlayerPrefs.GetInt("PlayerXp");
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("PlayerXp", PlayerControl.instance.xp);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
