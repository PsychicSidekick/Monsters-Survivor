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
        itemObj.description.SetActive(true);
        foreach (StatModifier mod in itemObj.itemModifiers)
        {
            PlayerControl.instance.GetComponent<StatsManager>().FindStatOfType(mod.statType).AddModifier(mod);
        }
        equippedItem = itemObj;
    }

    public void UnequipItem()
    {
        equippedItem.description.SetActive(false);
        foreach (StatModifier mod in equippedItem.itemModifiers)
        {
            PlayerControl.instance.GetComponent<StatsManager>().FindStatOfType(mod.statType).RemoveModifier(mod);
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
        if (!Inventory.instance.cursorItem && equippedItem)
        {
            equippedItem.description.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Inventory.instance.cursorItem && equippedItem)
        {
            equippedItem.description.SetActive(false);
        }
    }
}
