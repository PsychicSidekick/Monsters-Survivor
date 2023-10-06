using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemObj : MonoBehaviour
{
    public Item item;

    public ItemPrefab itemPrefab;

    public GameObject lootTextPrefab;
    public GameObject itemImgPrefab;
    public GameObject descriptionPrefab;
    public GameObject lootText;
    public GameObject itemImg;
    public GameObject description;

    public GameObject lootCanvas;
    public MeshRenderer mr;
    public Rigidbody rb;

    public Cell occupyingCell;

    public bool isEquipped = false;
    public bool isPickedUp = false;
    public bool isPlaced = false;

    private void Start()
    {
        lootCanvas = GameObject.Find("LootCanvas");
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        itemImgPrefab = itemPrefab.itemImgPrefab;

        lootText = Instantiate(lootTextPrefab, lootCanvas.transform);
        lootText.GetComponent<LootText>().itemObj = this;

        itemImg = Instantiate(itemImgPrefab, Inventory.instance.inventoryUI.transform);

        description = Instantiate(descriptionPrefab, Inventory.instance.descriptionHolder.transform);
        description.GetComponent<DescriptionPanel>().itemObj = this;
    }

    private void Update()
    {
        lootText.GetComponent<RectTransform>().anchoredPosition = GameManager.instance.WorldToCanvasPos(lootCanvas, transform.position);

        mr.enabled = !isPickedUp;
        lootText.SetActive(!isPickedUp);
        itemImg.SetActive(isPickedUp);
    }

    public void OnDrop()
    {
        transform.position = Player.instance.transform.position;
        rb.velocity = Vector3.zero;
    }
}
