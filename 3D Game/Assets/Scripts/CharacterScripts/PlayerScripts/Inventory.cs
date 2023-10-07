using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    [HideInInspector]
    public static Inventory instance;

    public Player player;

    [HideInInspector]
    public List<List<Cell>> inventory = new List<List<Cell>>();
    public Vector2Int inventorySize;

    [HideInInspector]
    public Item cursorItem;

    public GameObject descriptionPanel;

    public GameObject inventoryUI;

    public GameObject cellPrefab;
    public GameObject inventoryAnchor;


    // defines whether the player's current action is to pick up loot
    [HideInInspector] public bool pickingUpLoot;
    [HideInInspector] public bool lockCursor;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        InitiateInventory();
    }

    private void Update()
    {
        // Attach item image to cursor
        if (cursorItem != null)
        {
            cursorItem.itemImage.transform.position = Input.mousePosition;
        }

        if (pickingUpLoot)
        {
            Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
            Vector2 lootPos = new Vector2(player.targetLoot.transform.position.x, player.targetLoot.transform.position.z);
            // if player has arrived at the position of the target loot
            if (Vector2.Distance(playerPos, lootPos) < 0.1f)
            {
                if (inventoryUI.activeInHierarchy)
                {
                    PlaceItemInFirstAvailableCell(player.targetLoot.item);
                }
                else
                {
                    PlaceItemInFirstAvailableCell(player.targetLoot.item);
                }
            }
        }
        else if (cursorItem != null && lockCursor && Input.GetKeyDown(KeyCode.Mouse0) && !GameManager.IsMouseOverUI())
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

    public void PlaceItemInFirstAvailableCell(Item item)
    {
        PlaceItemInInventory(item, FindFirstAvailableCell(item.size));
    }

    public void PlaceItemInInventory(Item item, Cell c)
    {
        if (c == null)
        {
            return;
        }

        ResetCellsColour();

        pickingUpLoot = false;
        c.PlaceItem(item);
        item.itemImage.SetActive(true);
        if (item.lootGameObject)
        {
            Destroy(item.lootGameObject.gameObject);
        }
        
        item.itemImage.transform.position = FindItemImgPos(c, item.size);
        cursorItem = null;
    }

    public void PlaceItemInItemSlot(Item item, ItemSlot slot)
    {
        pickingUpLoot = false;
        slot.EquipItem(item);
        descriptionPanel.SetActive(true);
        descriptionPanel.GetComponent<DescriptionPanel>().UpdateDescription(item);

        item.itemImage.transform.position = slot.transform.position;
        cursorItem = null;
    }
    
    public void PickUpItemWithCursor(Item item)
    {
        if (item.occupiedCell != null)
        {
            item.occupiedCell.RemoveItem();
        }
        else
        {
            item.itemImage.SetActive(true);
        }

        descriptionPanel.SetActive(false);
        lockCursor = true;
        pickingUpLoot = false;
        cursorItem = item;
    }

    public void SwapCursorItemWithItemInInventory(Item item, Cell c)
    {
        Item inventoryItem = c.FindOccupyingItemInCells(c.FindCellGroupOfSize(item.size));
        PickUpItemWithCursor(inventoryItem);
        PlaceItemInInventory(item, c);
        descriptionPanel.SetActive(false);
        cursorItem = inventoryItem;
    }

    public void SwapCursorItemWithEquippedItem(Item item, ItemSlot slot)
    {
        Item equippedItem = slot.equippedItem;
        PickUpItemWithCursor(equippedItem);
        slot.UnequipItem();
        PlaceItemInItemSlot(item, slot);
        descriptionPanel.SetActive(false);
        cursorItem = equippedItem;
    }

    public void DropItem(Item item)
    {
        LootGameObject lootGameObject = Instantiate(item.itemBase.lootGameObjectPrefab, GameManager.instance.RefinedPos(player.transform.position), Quaternion.identity).GetComponent<LootGameObject>();
        lootGameObject.item = item;
        item.lootGameObject = lootGameObject;
        Destroy(item.itemImage);

        if (item.occupiedCell != null)
        {
            item.occupiedCell.RemoveItem();
        }

        cursorItem = null;
    }

    public void MovePlayerToLoot(LootGameObject lootGameObject)
    {
        if (lockCursor)
        {
            return;
        }

        lockCursor = true;
        pickingUpLoot = true;
        player.targetLoot = lootGameObject;
        player.Move(lootGameObject.transform.position);
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
}