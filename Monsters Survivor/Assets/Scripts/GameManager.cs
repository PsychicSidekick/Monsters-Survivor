using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Start time of current run
    [HideInInspector] public float runStartTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Sets start time of current run
        runStartTime = Time.time;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    public float GetCurrentRunTime()
    {
        return Time.time - runStartTime;
    }

    // Returns a string representing the given amount of seconds in the format of "00:00"
    public string TimeToString(float time)
    {
        string minutes = Mathf.Floor(time / 60).ToString().PadLeft(2, '0');

        string seconds = Mathf.Floor(time % 60).ToString().PadLeft(2, '0');

        return minutes + ":" + seconds;
    }

    // Returns the given vector3 with y = 1
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

    public static bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
