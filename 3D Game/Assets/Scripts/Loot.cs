using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{
    public Item item;
    public GameObject lootTextPrefab;
    public GameObject lootText;
    public GameObject lootCanvas;

    private void Start()
    {
        lootCanvas = GameObject.Find("LootCanvas");

        lootText = Instantiate(lootTextPrefab, new Vector2(0, 0), Quaternion.identity);
        lootText.GetComponent<LootText>().lootObj = gameObject;
        lootText.transform.SetParent(lootCanvas.transform);
    }

    private void Update()
    {
        lootText.GetComponent<RectTransform>().anchoredPosition = WorldToCanvasPos(transform.position);
    }

    public void OnDestroy()
    {
        Destroy(lootText);
    }

    public Vector2 WorldToCanvasPos(Vector3 worldPos)
    {
        RectTransform canvasRect = lootCanvas.GetComponent<RectTransform>();

        Vector2 viewportPos = Camera.main.WorldToViewportPoint(worldPos);
        Vector2 canvasPos = new Vector2(
        (viewportPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
        (viewportPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));

        return canvasPos;
    }
}
