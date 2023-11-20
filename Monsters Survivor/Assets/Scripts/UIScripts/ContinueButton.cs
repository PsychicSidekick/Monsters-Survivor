using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ContinueButton : MonoBehaviour
{
    void Start()
    {
        if (!Application.streamingAssetsPath.Contains("://") && !Application.streamingAssetsPath.Contains(":///"))
        {
            string json = File.ReadAllText(Application.streamingAssetsPath + "/Save/save.txt");

            if (json.Length < 72 && PlayerPrefs.GetFloat("HighScore") == 0)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            // In WebGL
            gameObject.SetActive(false);
        }
    }
}
