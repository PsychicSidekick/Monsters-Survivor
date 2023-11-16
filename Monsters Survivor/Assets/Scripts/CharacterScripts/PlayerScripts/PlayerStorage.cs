using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerStorage : MonoBehaviour
{
    public static PlayerStorage instance;

    public Player player;
    public GameObject descriptionPanel;
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public GameObject cellPrefab;

    [HideInInspector] public List<List<Cell>> inventory = new List<List<Cell>>();
    public Vector2Int inventorySize;
    public GameObject inventoryAnchor;
    public GameObject inventoryCells;

    [HideInInspector] public List<List<Cell>> stash = new List<List<Cell>>();
    public Vector2Int stashSize;
    public GameObject stashAnchor;
    public GameObject stashCells;

    // defines whether the player's current action is to pick up loot
    [HideInInspector] public bool pickingUpLoot;
    [HideInInspector] public bool lockCursor;
    [HideInInspector] public Item cursorItem;

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
        InitiateStorage();
        GameSave storageSave = new GameSave();
        storageSave.Load();
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
            if (Player.instance == null)
            {
                pickingUpLoot = false;
                return;
            }
            Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
            Vector2 lootPos = new Vector2(player.targetLoot.transform.position.x, player.targetLoot.transform.position.z);
            // If player has arrived at the position of the target loot
            if (Vector2.Distance(playerPos, lootPos) < 0.1f)
            {
                if (PlaceItemInFirstAvailableCell(inventory, player.targetLoot.item))
                {
                    GetComponent<AudioSource>().Play();
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

    public void InitiateStorage()
    {
        for (int x = 0; x < inventorySize.x; x++)
        {
            List<Cell> cellCol = new List<Cell>();

            for (int y = 0; y < inventorySize.y; y++)
            {
                Cell c = Instantiate(cellPrefab, inventoryAnchor.transform.position + new Vector3(x * 60, y * -60, 0), Quaternion.identity).GetComponent<Cell>();
                c.pos = new Vector2Int(x, y);
                c.storage = inventory;
                c.occupied = false;
                c.transform.SetParent(inventoryCells.transform);
                cellCol.Add(c);
            }

            inventory.Add(cellCol);
        }

        for (int x = 0; x < stashSize.x; x++)
        {
            List<Cell> cellCol = new List<Cell>();

            for (int y = 0; y < stashSize.y; y++)
            {
                Cell c = Instantiate(cellPrefab, stashAnchor.transform.position + new Vector3(x * 60, y * -60, 0), Quaternion.identity).GetComponent<Cell>();
                c.pos = new Vector2Int(x, y);
                c.storage = stash;
                c.occupied = false;
                c.transform.SetParent(stashCells.transform);
                cellCol.Add(c);
            }

            stash.Add(cellCol);
        }
    }

    public void CleanStorage()
    {
        for (int x = 0; x < stashSize.x; x++)
        {
            for (int y = 0; y < stashSize.y; y++)
            {
                Cell cell = stash[x][y];
                if (cell.childCells != null && cell.childCells.Count > 0)
                {
                    Destroy(cell.occupiedBy.itemImage);
                    cell.RemoveItem();
                }
            }
        }

        for (int x = 0; x < inventorySize.x; x++)
        {
            for (int y = 0; y < inventorySize.y; y++)
            {
                Cell cell = inventory[x][y];
                if (cell.childCells != null && cell.childCells.Count > 0)
                {
                    Destroy(cell.occupiedBy.itemImage);
                    cell.RemoveItem();
                }
            }
        }

        foreach (ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.equippedItem != null)
            {
                Destroy(itemSlot.equippedItem.itemImage);
                itemSlot.equippedItem = null;
            }
        }
    }

    public Cell FindFirstAvailableCell(List<List<Cell>> cells, Vector2Int itemSize)
    {
        for (int x = 0; x < cells.Count; x++)
        {
            for (int y = 0; y < cells[0].Count; y++)
            {
                Cell cell = cells[x][y];

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

    public bool PlaceItemInFirstAvailableCell(List<List<Cell>> cells, Item item)
    {
        return PlaceItem(item, FindFirstAvailableCell(cells, item.itemBase.size));
    }

    public bool PlaceItem(Item item, Cell c)
    {
        if (c == null)
        {
            return false;
        }

        ResetCellsColour();

        pickingUpLoot = false;
        c.PlaceItem(item);
        item.itemImage.SetActive(true);
        if (item.lootGameObject)
        {
            Destroy(item.lootGameObject.gameObject);
        }
        
        item.itemImage.transform.position = FindItemImgPos(c, item.itemBase.size);
        item.itemImage.transform.SetParent(c.transform.parent);
        cursorItem = null;
        return true;
    }

    public void PlaceItemInItemSlot(Item item, ItemSlot slot)
    {
        pickingUpLoot = false;
        slot.EquipItem(item);
        descriptionPanel.SetActive(true);
        descriptionPanel.GetComponent<DescriptionPanel>().UpdateDescription(item);

        item.itemImage.transform.position = slot.transform.position;
        item.itemImage.transform.SetParent(slot.transform);
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

        item.itemImage.transform.SetParent(transform);
        descriptionPanel.SetActive(false);
        lockCursor = true;
        pickingUpLoot = false;
        cursorItem = item;
    }

    public void SwapCursorItemWithItemInInventory(Item item, Cell cellContainingItem, Cell cellClickedOn)
    {
        Item inventoryItem = cellContainingItem.FindOccupyingItemInCells(cellContainingItem.FindCellGroupOfSize(item.itemBase.size));
        PickUpItemWithCursor(inventoryItem);
        PlaceItem(item, cellContainingItem);
        descriptionPanel.SetActive(true);
        cursorItem = inventoryItem;
        descriptionPanel.GetComponent<DescriptionPanel>().UpdateDescription(item);
        cellClickedOn.UpdateCellColour();
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

        for (int x = 0; x < stashSize.x; x++)
        {
            for (int y = 0; y < stashSize.y; y++)
            {
                stash[x][y].state = CellState.Normal;
            }
        }
    }

    public void InstantiateItemImage(Item item)
    {
        item.itemImage = Instantiate(item.itemBase.itemImagePrefab, transform);
    }

    public Vector3 FindItemImgPos(Cell cell, Vector2Int itemSize)
    {
        float xPos = cell.transform.position.x + (itemSize.x - 1) * 30;
        float yPos = cell.transform.position.y + (itemSize.y - 1) * -30;

        return new Vector3(xPos, yPos, 0);
    }

    private void OnApplicationQuit()
    {
        GameSave storageSave = new GameSave();
        storageSave.Save();
    }
}