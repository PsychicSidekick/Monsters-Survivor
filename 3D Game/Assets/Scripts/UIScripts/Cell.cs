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
    public List<List<Cell>> storage;
    public bool occupied;
    public Item occupiedBy;
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

    public void PlaceItem(Item item)
    {
        item.occupiedCell = this;

        childCells = FindCellGroupOfSize(item.size);
        if(childCells == null)
        {
            Debug.Log("Item cannot be placed in this parent cell");
        }

        foreach (Cell c in childCells)
        {
            c.occupied = true;
            c.occupiedBy = item;
        }
    }

    public void RemoveItem()
    {
        occupiedBy.occupiedCell = null;

        foreach (Cell c in childCells)
        {
            c.occupied = false;
            c.occupiedBy = null;
        }

        childCells = null;
    }

    public bool CanFitItem(Vector2Int itemSize)
    {
        if(ExeedsStorageSize(itemSize))
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

    public bool ExeedsStorageSize(Vector2Int itemSize)
    {
        return pos.x + itemSize.x > storage.Count || pos.y + itemSize.y > storage[0].Count;
    }

    public List<Cell> FindCellGroupOfSize(Vector2Int itemSize)
    {
        if(ExeedsStorageSize(itemSize))
        {
            return null;
        }

        List<Cell> cells = new List<Cell>();

        for (int x = 0; x < itemSize.x; x++)
        {
            for (int y = 0; y < itemSize.y; y++)
            {
                cells.Add(storage[pos.x + x][pos.y + y]);
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
        else if (xPos + itemSize.x > storage.Count)
        {
            xOffSet += xPos + itemSize.x - storage.Count;
        }

        if (yPos < 0)
        {
            yOffSet = 0;
        }
        else if (yPos + itemSize.y > storage[0].Count)
        {
            yOffSet += yPos + itemSize.y - storage[0].Count;
        }

        return storage[pos.x - xOffSet][pos.y - yOffSet];
    }

    public int CountItemsInCells(List<Cell> cells)
    {
        return cells.Select(cell => cell.occupiedBy)
                    .Where(itemObj => itemObj is not null)
                    .GroupBy(itemObj => itemObj)
                    .Count();
    }

    public Item FindOccupyingItemInCells(List<Cell> cells)
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

        Item cursorItem = PlayerStorage.instance.cursorItem;

        if (cursorItem == null)
        {
            if (occupiedBy != null)
            {
                PlayerStorage.instance.PickUpItemWithCursor(occupiedBy);
            }
            return;
        }

        Cell parentCell = FindParentCell(cursorItem.size, Input.mousePosition);
        List<Cell> cellGroup = parentCell.FindCellGroupOfSize(cursorItem.size);

        int overlappedItems = CountItemsInCells(cellGroup);

        if (overlappedItems > 1)
        {
            return;
        }
        else
        {
            Item temp = cursorItem;

            if (overlappedItems == 1)
            {
                PlayerStorage.instance.SwapCursorItemWithItemInInventory(cursorItem, parentCell);
                return;
            }
                
            PlayerStorage.instance.PlaceItem(temp, parentCell);
            PlayerStorage.instance.descriptionPanel.SetActive(true);
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Item cursorItem = PlayerStorage.instance.cursorItem;

        if (occupiedBy != null)
        {
            PlayerStorage.instance.descriptionPanel.SetActive(true);
            PlayerStorage.instance.descriptionPanel.GetComponent<DescriptionPanel>().UpdateDescription(occupiedBy);
        }

        if (cursorItem == null)
        {
            return;
        }

        PlayerStorage.instance.ResetCellsColour();

        List<Cell> cellGroup = FindParentCell(cursorItem.size, Input.mousePosition).FindCellGroupOfSize(cursorItem.size);

        if (CountItemsInCells(cellGroup) > 1)
        {
            PlayerStorage.instance.ChangeCellsStates(cellGroup, CellState.CannotFit);
        }
        else
        {
            PlayerStorage.instance.ChangeCellsStates(cellGroup, CellState.Highlighted);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (occupiedBy != null)
        {
            PlayerStorage.instance.descriptionPanel.SetActive(false);
        }

        if (pos.x == 0 || pos.y == 0)
        {
            PlayerStorage.instance.ResetCellsColour();
        }
    }
}
