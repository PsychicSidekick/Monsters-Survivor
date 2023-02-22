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

    public List<StatModifier> itemModifiers = new List<StatModifier>();

    private void Start()
    {
        lootCanvas = GameObject.Find("LootCanvas");
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        item = new Item(itemPrefab);

        itemImgPrefab = itemPrefab.itemImgPrefab;

        lootText = Instantiate(lootTextPrefab, lootCanvas.transform);
        lootText.GetComponent<LootText>().itemObj = this;

        itemImg = Instantiate(itemImgPrefab, Inventory.instance.inventoryUI.transform);

        description = Instantiate(descriptionPrefab, Inventory.instance.descriptionHolder.transform);
        description.GetComponent<DescriptionPanel>().itemObj = this;

        StatModifier mod = new StatModifier(StatType.ManaRegen, 1, ModType.Flat);
        StatModifier mod1 = new StatModifier(StatType.MaxLife, 100, ModType.Inc);
        StatModifier mod2 = new StatModifier(StatType.MoveSpd, 10, ModType.More);
        itemModifiers.Add(mod);
        itemModifiers.Add(mod1);
        itemModifiers.Add(mod2);
    }

    private void Update()
    {
        lootText.GetComponent<RectTransform>().anchoredPosition = WorldToCanvasPos(transform.position);

        mr.enabled = !isPickedUp;
        lootText.SetActive(!isPickedUp);
        itemImg.SetActive(isPickedUp);
        //itemImg.transform.SetParent(isPlaced ? Inventory.instance.inventoryUI.transform : Inventory.instance.transform);
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
