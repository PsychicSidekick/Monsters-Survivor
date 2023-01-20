using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObj : MonoBehaviour
{
    public Item item;

    public ItemPrefab itemPrefab;

    public GameObject lootTextPrefab;
    public GameObject itemImgPrefab;
    public GameObject lootText;
    public GameObject itemImg;

    public GameObject lootCanvas;
    public MeshRenderer mr;
    public Rigidbody rb;

    public Cell occupyingCell;

    public bool isPickedUp = false;
    public bool isPlaced = false;

    private void Start()
    {
        lootCanvas = GameObject.Find("LootCanvas");
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        item = new Item(itemPrefab);

        itemImgPrefab = itemPrefab.itemImgPrefab;

        lootText = Instantiate(lootTextPrefab, lootCanvas.transform);
        lootText.GetComponent<LootText>().itemObj = this;

        itemImg = Instantiate(itemImgPrefab, Inventory.instance.transform);
    }

    private void Update()
    {
        lootText.GetComponent<RectTransform>().anchoredPosition = WorldToCanvasPos(transform.position);

        mr.enabled = !isPickedUp;
        lootText.SetActive(!isPickedUp);
        itemImg.SetActive(isPickedUp);
        itemImg.transform.SetParent(isPlaced ? Inventory.instance.inventoryUI.transform : Inventory.instance.transform);
    }

    public void OnDrop()
    {
        transform.position = PlayerControl.instance.transform.position;
        rb.velocity = Vector3.zero;
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
