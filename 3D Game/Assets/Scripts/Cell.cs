using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerMoveHandler
{
    public Vector2Int pos;
    public bool occupied;
    public ItemObj occupiedBy;
    List<Cell> childCells = new List<Cell>();

    public void PlaceItem(ItemObj itemObj)
    {
        itemObj.occupyingCell = this;

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

    public void OnPointerMove(PointerEventData eventData)
    {
        //Debug.Log(transform.position + "/" + Time.time);
        //Debug.Log(Input.mousePosition);
    }
}
