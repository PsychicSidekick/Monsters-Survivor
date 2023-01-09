using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<List<Cell>> inventory = new List<List<Cell>>();
    public Vector2Int inventorySize = new Vector2Int(8, 6);
    public List<Item> itemList = new List<Item>();
    public Dictionary<ItemType, ItemSlot> itemSlots = new Dictionary<ItemType, ItemSlot>();

    public ItemSlot weaponSlot;
    public ItemSlot helmetSlot;
    public ItemSlot bodySlot;

    public List<GameObject> itemBtns = new List<GameObject>();
    public GameObject inventoryUI;

    public TMP_Text label;

    public Button itemBtnPrefab;
    public GameObject cellPrefab;
    public GameObject inventoryAnchor;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitiateInventory();

        itemSlots.Add(ItemType.Weapon, weaponSlot);
        itemSlots.Add(ItemType.Helmet, helmetSlot);
        itemSlots.Add(ItemType.Body, bodySlot);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
        }
    }

    private void InitiateInventory()
    {
        for (int x = 0; x < inventorySize.x; x++)
        {
            List<Cell> cellCol = new List<Cell>();

            for (int y = 0; y < inventorySize.y; y++)
            {
                Cell c = Instantiate(cellPrefab, inventoryAnchor.transform.position + new Vector3(x * 80, y * -80, 0), Quaternion.identity).GetComponent<Cell>();
                c.pos = new Vector2Int(x, y);
                c.occupied = false;
                c.transform.SetParent(inventoryUI.transform);
                cellCol.Add(c);
            }

            inventory.Add(cellCol);
        }
    }

    private Cell FindFirstAvailableCell(Vector2Int itemSize)
    {
        for (int x = 0; x < inventorySize.x; x++)
        {
            for (int y = 0; y < inventorySize.y; y++)
            {
                Cell cell = inventory[x][y];

                if (cell.occupied)
                {
                    continue;
                }

                if (!cell.CanFitItem(itemSize))
                {
                    continue;
                }

                return cell;
            }
        }

        Debug.Log("No space from FindFirstAvailableCell in Inventory");
        return null;
    }

    public void AddItem(Item item)
    {
        Cell c = FindFirstAvailableCell(item.size);
        c.PlaceItem(item);
        //itemList.Add(item);
        UpdateUI();
    }

    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
        UpdateUI();
    }

    public void EquipItem(Item item)
    {
        itemSlots[item.type].Equip(item);
        RemoveItem(item);
    }

    public void UnequipItem(Item item)
    {
        AddItem(item);
    }

    public void UpdateUI()
    {
        foreach (GameObject itemBtn in itemBtns)
        {
            Destroy(itemBtn);
        }

        itemBtns.Clear();

        for (int i = 0; i < itemList.Count; i++)
        {
            Button iBtn = Instantiate(itemBtnPrefab, label.transform.position + new Vector3(0, -40 * (i + 1), 0), Quaternion.identity);
            iBtn.GetComponent<ItemButton>().item = itemList[i];
            itemBtns.Add(iBtn.gameObject);
            iBtn.transform.SetParent(inventoryUI.transform);

        }
    }
}