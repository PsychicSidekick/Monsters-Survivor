using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public PlayerControl player;

    public List<List<Cell>> inventory = new List<List<Cell>>();
    public Vector2Int inventorySize = new Vector2Int(8, 6);
    public List<Item> itemList = new List<Item>();
    public Dictionary<ItemType, ItemSlot> itemSlots = new Dictionary<ItemType, ItemSlot>();

    public ItemObj cursorItem;

    public ItemSlot weaponSlot;
    public ItemSlot helmetSlot;
    public ItemSlot bodySlot;

    public List<GameObject> itemBtns = new List<GameObject>();
    public GameObject inventoryUI;

    public TMP_Text label;

    public Button itemBtnPrefab;
    public GameObject cellPrefab;
    public GameObject inventoryAnchor;
    public GameObject descriptionHolder;

    public bool pickingUpLoot;
    public bool lockCursor;

    private void Awake()
    {
        instance = this;
        player = PlayerControl.instance;
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
        // Attach item image to cursor
        if (cursorItem != null)
        {
            cursorItem.itemImg.transform.position = Input.mousePosition;
        }

        // Toggle inventory UI
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
        }

        if (pickingUpLoot)
        {
            if (Vector3.Distance(player.transform.position, player.targetItem.transform.position) < 0.1f)
            {
                if (inventoryUI.activeInHierarchy)
                {
                    PickUpItemWithCursor(player.targetItem.GetComponent<ItemObj>());
                }
                else
                {
                    PlaceItemInInventory(player.targetItem.GetComponent<ItemObj>(), FindFirstAvailableCell(player.targetItem.GetComponent<ItemObj>().item.size));
                }
            }
        }
        else if (cursorItem != null && lockCursor && Input.GetKeyDown(KeyCode.Mouse0) && !IsMouseOverUI())
        {
            DropItem(cursorItem);
        }

        if (cursorItem == null && lockCursor && Input.GetKeyUp(KeyCode.Mouse0))
        {
            lockCursor = false;
        }
    }

    private void InitiateInventory()
    {
        for (int x = 0; x < inventorySize.x; x++)
        {
            List<Cell> cellCol = new List<Cell>();

            for (int y = 0; y < inventorySize.y; y++)
            {
                Cell c = Instantiate(cellPrefab, inventoryAnchor.transform.position + new Vector3(x * 60, y * -60, 0), Quaternion.identity).GetComponent<Cell>();
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

    public void PlaceItemInInventory(ItemObj itemObj, Cell c)
    {
        ResetCellsColour();

        pickingUpLoot = false;
        c.PlaceItem(itemObj);

        itemObj.isPickedUp = true;
        itemObj.isPlaced = true;
        itemObj.itemImg.transform.position = FindItemImgPos(c, itemObj.item.size);
        cursorItem = null;
    }

    public void PlaceItemInItemSlot(ItemObj itemObj, ItemSlot slot)
    {
        pickingUpLoot = false;
        slot.EquipItem(itemObj);

        itemObj.isPickedUp = true;
        itemObj.isPlaced = true;
        itemObj.itemImg.transform.position = slot.transform.position;
        cursorItem = null;
    }
    
    public void PickUpItemWithCursor(ItemObj itemObj)
    {
        if (itemObj.occupyingCell != null)
        {
            itemObj.occupyingCell.RemoveItem();
        }

        lockCursor = true;
        pickingUpLoot = false;
        itemObj.isPickedUp = true;
        itemObj.isPlaced = false;
        cursorItem = itemObj;
    }

    public void SwapItemWithInventoryItem(ItemObj itemObj, Cell c)
    {
        ItemObj inventoryItem = c.FindOccupyingItemInCells(c.FindCellGroupOfSize(itemObj.item.size));
        PickUpItemWithCursor(inventoryItem);
        PlaceItemInInventory(itemObj, c);
        itemObj.description.SetActive(false);
        cursorItem = inventoryItem;
    }

    public void SwapItemWithEquippedItem(ItemObj itemObj, ItemSlot slot)
    {
        ItemObj equippedItem = slot.equippedItem;
        PickUpItemWithCursor(equippedItem);
        slot.UnequipItem();
        PlaceItemInItemSlot(itemObj, slot);
        itemObj.description.SetActive(false);
        cursorItem = equippedItem;
    }

    public void DropItem(ItemObj itemObj)
    {
        itemObj.OnDrop();
        if (itemObj.isPlaced)
        {
            itemObj.occupyingCell.RemoveItem();
        }
        itemObj.isPlaced = false;
        itemObj.isPickedUp = false;
        cursorItem = null;
    }

    public void MovePlayerToLoot(GameObject itemObj)
    {
        if (lockCursor)
        {
            return;
        }

        lockCursor = true;
        pickingUpLoot = true;
        player.targetItem = itemObj;
        player.Move(itemObj.transform.position);
    }

    public void ResetCellsColour()
    {
        for (int x = 0; x < inventorySize.x; x++)
        {
            for (int y = 0; y < inventorySize.y; y++)
            {
                inventory[x][y].state = CellState.Normal;
            }
        }
    }

    public void ChangeCellsStates(List<Cell> cellsToBeHighlighted, CellState state)
    {
        foreach (Cell cell in cellsToBeHighlighted)
        {
            cell.state = state;
        }
    }

    public Vector3 FindItemImgPos(Cell cell, Vector2Int itemSize)
    {
        float xPos = cell.transform.position.x + (itemSize.x - 1) * 30;
        float yPos = cell.transform.position.y + (itemSize.y - 1) * -30;

        return new Vector3(xPos, yPos, 0);
    }

    public bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}