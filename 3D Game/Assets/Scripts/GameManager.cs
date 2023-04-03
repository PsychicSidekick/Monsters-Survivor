using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

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

    public Vector3 RefinedPos(Vector3 position)
    {
        return new Vector3(position.x, 1, position.z);
    }

    public Vector2 WorldToCanvasPos(GameObject canvas, Vector3 worldPos)
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        Vector2 viewportPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector2 canvasPos = new Vector2(
        (viewportPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
        (viewportPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));

        return canvasPos;
    }
}
