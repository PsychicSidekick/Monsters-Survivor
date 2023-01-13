using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemImg : MonoBehaviour, IPointerDownHandler
{
    public ItemObj itemObj;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (itemObj.isPlaced && eventData.pointerId == -2)
        {
            Inventory.instance.DropItem(itemObj);
        }
    }
}
