using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ItemType slotType;
    public Item equippedItem;

    public void EquipItem(Item item)
    {
        item.isEquipped = true;
        foreach (StatModifier mod in item.itemModifiers)
        {
            Player.instance.stats.ApplyStatModifier(mod);
        }
        equippedItem = item;
    }

    public void UnequipItem()
    {
        equippedItem.isEquipped = false;
        foreach (StatModifier mod in equippedItem.itemModifiers)
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

        Item cursorItem = Inventory.instance.cursorItem;

        if (cursorItem != null && cursorItem.type == slotType)
        {
            if (equippedItem == null)
            {
                Inventory.instance.PlaceItemInItemSlot(cursorItem, this);
                return;
            }
            else
            {
                Inventory.instance.SwapCursorItemWithEquippedItem(cursorItem, this);
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
        if (equippedItem != null)
        {
            Inventory.instance.descriptionPanel.SetActive(true);
            Inventory.instance.descriptionPanel.GetComponent<DescriptionPanel>().UpdateDescription(equippedItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (equippedItem != null)
        {
            Inventory.instance.descriptionPanel.SetActive(false);
        }
    }
}
