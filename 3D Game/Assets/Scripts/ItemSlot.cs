using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public ItemType slotType;
    public Item equippedItem;
    public TMP_Text itemName;

    public void Equip(Item item)
    {
        equippedItem = item;
        itemName.text = item.itemName;
    }

    public void Unequip()
    {
        Inventory.instance.UnequipItem(equippedItem);
        equippedItem = null;
        itemName.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(equippedItem == null)
        {
            return;
        }

        if (eventData.pointerId == -1)
        {
            Unequip();
        }
    }
}
