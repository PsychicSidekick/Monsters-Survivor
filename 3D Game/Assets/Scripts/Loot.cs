using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{
    public Item item;
    public GameObject pickUpBtnPrefab;
    public RectTransform pickUpBtn;
    public GameObject lootCanvas;

    private void Start()
    {
        lootCanvas = GameObject.Find("LootCanvas");

        GameObject btn = Instantiate(pickUpBtnPrefab, new Vector2(0, 0), Quaternion.identity);
        pickUpBtn = btn.GetComponent<RectTransform>();
        pickUpBtn.transform.SetParent(lootCanvas.transform);
    }

    private void Update()
    {
        pickUpBtn.anchoredPosition = WorldToCanvasPos(transform.position);
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
