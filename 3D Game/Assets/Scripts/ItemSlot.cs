using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public ItemType slotType;
    public ItemObj equippedItem;

    public void EquipItem(ItemObj itemObj)
    {
        equippedItem = itemObj;
    }

    public void UnequipItem()
    {
        equippedItem = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId != -1)
        {
            return;
        }

        ItemObj cursorItem = Inventory.instance.cursorItem;

        if (cursorItem != null && cursorItem.item.type == slotType)
        {
            if (equippedItem == null)
            {
                Inventory.instance.PlaceItemInItemSlot(cursorItem, this);
                return;
            }
            else
            {
                Inventory.instance.SwapItemWithEquippedItem(cursorItem, this);
                return;
            }
        }

        if (equippedItem != null)
        {
            Inventory.instance.PickUpItemWithCursor(equippedItem);
            UnequipItem();
            return;
        }
    }
}
