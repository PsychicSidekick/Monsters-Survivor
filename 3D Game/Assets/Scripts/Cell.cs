using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerMoveHandler
{
    public Vector2Int pos;
    public bool occupied;

    public void PlaceItem(Item item)
    {
        List<Cell> childCells = FindCellGroupOfSize(item.size);
        if(childCells == null)
        {
            Debug.Log("Item cannot be placed in this parent cell");
        }

        foreach (Cell c in childCells)
        {
            c.occupied = true;
        }

        Image itemImg = Instantiate(item.itemImage, FindItemImgPos(item.size), Quaternion.identity);
        itemImg.transform.SetParent(Inventory.instance.inventoryUI.transform);
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

    public Vector3 FindItemImgPos(Vector2Int itemSize)
    {
        float xPos = transform.position.x + (itemSize.x - 1) * 40;
        float yPos = transform.position.y + (itemSize.y - 1) * -40;

        return new Vector3(xPos, yPos, 0);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Debug.Log(transform.position + "/" + Time.time);
        //Debug.Log(Input.mousePosition);
    }
}
