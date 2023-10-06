using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemType slotType;
    public ItemObj equippedItem;

    public void EquipItem(ItemObj itemObj)
    {
        itemObj.isEquipped = true;
        itemObj.description.SetActive(true);
        foreach (StatModifier mod in itemObj.item.itemModifiers)
        {
            Player.instance.stats.ApplyStatModifier(mod);
        }
        equippedItem = itemObj;
    }

    public void UnequipItem()
    {
        equippedItem.isEquipped = false;
        equippedItem.description.SetActive(false);
        foreach (StatModifier mod in equippedItem.item.itemModifiers)
        {
            Player.instance.stats.RemoveStatModifier(mod);
        }
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedItem)
        {
            equippedItem.description.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (equippedItem)
        {
            equippedItem.description.SetActive(false);
        }
    }
}
