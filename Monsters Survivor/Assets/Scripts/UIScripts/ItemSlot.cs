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
        if (Player.instance != null)
        {
            foreach (StatModifier mod in item.itemModifiers)
            {
                Player.instance.stats.ApplyStatModifier(mod);
            }
        }

        equippedItem = item;
    }

    public void UnequipItem()
    {
        if (Player.instance != null)
        {
            foreach (StatModifier mod in equippedItem.itemModifiers)
            {
                Player.instance.stats.RemoveStatModifier(mod);
            }
        }

        equippedItem = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId != -1)
        {
            return;
        }

        Item cursorItem = PlayerStorage.instance.cursorItem;

        if (cursorItem != null && cursorItem.itemBase.type == slotType)
        {
            if (equippedItem == null)
            {
                PlayerStorage.instance.PlaceItemInItemSlot(cursorItem, this);
                return;
            }
            else
            {
                PlayerStorage.instance.SwapCursorItemWithEquippedItem(cursorItem, this);
                return;
            }
        }

        if (equippedItem != null)
        {
            PlayerStorage.instance.PickUpItemWithCursor(equippedItem);
            UnequipItem();
            return;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedItem != null)
        {
            PlayerStorage.instance.descriptionPanel.SetActive(true);
            PlayerStorage.instance.descriptionPanel.GetComponent<DescriptionPanel>().UpdateDescription(equippedItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (equippedItem != null)
        {
            PlayerStorage.instance.descriptionPanel.SetActive(false);
        }
    }
}
