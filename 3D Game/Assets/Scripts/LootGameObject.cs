using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootGameObject : MonoBehaviour
{
    public Item item;

    public GameObject lootButtonPrefab;
    private GameObject lootButton;

    private GameObject lootCanvas;

    private void Start()
    {
        lootCanvas = GameObject.Find("LootCanvas");

        lootButton = Instantiate(lootButtonPrefab, lootCanvas.transform);
        lootButton.GetComponent<LootButton>().lootGameObject = this;

        item.itemImage = Instantiate(item.itemBase.itemImagePrefab, PlayerStorage.instance.inventoryCells.transform);
        item.itemImage.SetActive(false);
    }

    private void Update()
    {
        lootButton.GetComponent<RectTransform>().anchoredPosition = GameManager.instance.WorldToCanvasPos(lootCanvas, transform.position);
    }

    private void OnDestroy()
    {
        Destroy(lootButton);
    }
}
