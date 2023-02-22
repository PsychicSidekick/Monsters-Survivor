using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum CellState
{
    Normal,
    Highlighted,
    CannotFit
}

public class Cell : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler, IPointerClickHandler
{
    public Vector2Int pos;
    public bool occupied;
    public ItemObj occupiedBy;
    List<Cell> childCells = new List<Cell>();

    public CellState state;

    private void Update()
    {
        switch(state)
        {
            case CellState.Normal:
                GetComponent<Image>().color = Color.white;
                break;
            case CellState.Highlighted:
                GetComponent<Image>().color = Color.green;
                break;
            case CellState.CannotFit:
                GetComponent<Image>().color = Color.red;
                break;
        }
    }

    public void PlaceItem(ItemObj itemObj)
    {
        itemObj.occupyingCell = this;

        if (Inventory.instance.inventoryUI.activeInHierarchy)
        {
            itemObj.description.SetActive(true);
        }

        childCells = FindCellGroupOfSize(itemObj.item.size);
        if(childCells == null)
        {
            Debug.Log("Item cannot be placed in this parent cell");
        }

        foreach (Cell c in childCells)
        {
            c.occupied = true;
            c.occupiedBy = itemObj;
        }
    }

    public void RemoveItem()
    {
        occupiedBy.occupyingCell = null;
        occupiedBy.description.SetActive(false);

        foreach (Cell c in childCells)
        {
            c.occupied = false;
            c.occupiedBy = null;
        }

        childCells = null;
    }

    public bool CanFitItem(Vector2Int itemSize)
    {
        if(ExeedsInventorySize(itemSize))
        {
            return false;
        }

        List<Cell> cells = FindCellGroupOfSize(itemSize);

        foreach (Cell c in cells)
        {
            if(c.occupied)
            {
                return false;
            }
        }

        return true;
    }

    public bool ExeedsInventorySize(Vector2Int itemSize)
    {
        return pos.x + itemSize.x > Inventory.instance.inventorySize.x || pos.y + itemSize.y > Inventory.instance.inventorySize.y;
    }

    public List<Cell> FindCellGroupOfSize(Vector2Int itemSize)
    {
        if(ExeedsInventorySize(itemSize))
        {
            return null;
        }

        List<Cell> cells = new List<Cell>();

        for (int x = 0; x < itemSize.x; x++)
        {
            for (int y = 0; y < itemSize.y; y++)
            {
                cells.Add(Inventory.instance.inventory[pos.x + x][pos.y + y]);
            }
        }

        return cells;
    }

    public Cell FindParentCell(Vector2Int itemSize, Vector3 mousePos)
    {
        int xOffSet;
        int yOffSet;

        if (itemSize.x % 2 == 1)
        {
            xOffSet = (itemSize.x - 1) / 2;
        }
        else
        {
            xOffSet = mousePos.x < transform.position.x ? itemSize.x / 2 : 0;
        }

        if (itemSize.y % 2 == 1)
        {
            yOffSet = (itemSize.y - 1) / 2;
        }
        else
        {
            yOffSet = mousePos.y > transform.position.y ? itemSize.y / 2 : 0;
        }

        int xPos = pos.x - xOffSet;
        int yPos = pos.y - yOffSet;

        if (xPos < 0)
        {
            xOffSet = 0;
        }
        else if (xPos + itemSize.x > Inventory.instance.inventorySize.x)
        {
            xOffSet += xPos + itemSize.x - Inventory.instance.inventorySize.x;
        }

        if (yPos < 0)
        {
            yOffSet = 0;
        }
        else if (yPos + itemSize.y > Inventory.instance.inventorySize.y)
        {
            yOffSet += yPos + itemSize.y - Inventory.instance.inventorySize.y;
        }

        return Inventory.instance.inventory[pos.x - xOffSet][pos.y - yOffSet];
    }

    public int CountItemsInCells(List<Cell> cells)
    {
        return cells.Select(cell => cell.occupiedBy)
                    .Where(itemObj => itemObj is not null)
                    .GroupBy(itemObj => itemObj)
                    .Count();
    }

    public ItemObj FindOccupyingItemInCells(List<Cell> cells)
    {
        if (CountItemsInCells(cells) > 1)
        {
            Debug.Log("Provided cells contain more than one item");
            return null;
        }

        foreach (Cell cell in cells)
        {
            if (cell.occupiedBy != null)
            {
                return cell.occupiedBy;
            }
        }

        return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId != -1)
        {
            return;
        }

        ItemObj cursorItem = Inventory.instance.cursorItem;

        if (cursorItem == null)
        {
            if (occupiedBy != null)
            {
                Inventory.instance.PickUpItemWithCursor(occupiedBy);
            }
            return;
        }

        Cell parentCell = FindParentCell(cursorItem.item.size, Input.mousePosition);
        List<Cell> cellGroup = parentCell.FindCellGroupOfSize(cursorItem.item.size);

        int overlappedItems = CountItemsInCells(cellGroup);

        if (overlappedItems > 1)
        {
            return;
        }
        else
        {
            ItemObj temp = cursorItem;

            if (overlappedItems == 1)
            {
                Inventory.instance.SwapItemWithInventoryItem(cursorItem, parentCell);
                return;
            }
                
            Inventory.instance.PlaceItemInInventory(temp, parentCell);

        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        ItemObj cursorItem = Inventory.instance.cursorItem;

        if (occupiedBy)
        {
            occupiedBy.description.SetActive(true);
        }

        if (cursorItem == null)
        {
            return;
        }

        Inventory.instance.ResetCellsColour();

        List<Cell> cellGroup = FindParentCell(cursorItem.item.size, Input.mousePosition).FindCellGroupOfSize(cursorItem.item.size);

        if (CountItemsInCells(cellGroup) > 1)
        {
            Inventory.instance.ChangeCellsStates(cellGroup, CellState.CannotFit);
        }
        else
        {
            Inventory.instance.ChangeCellsStates(cellGroup, CellState.Highlighted);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (occupiedBy != null)
        {
            occupiedBy.description.SetActive(false);
        }

        if (pos.x == 0 || pos.y == 0)
        {
            Inventory.instance.ResetCellsColour();
        }
    }
}
