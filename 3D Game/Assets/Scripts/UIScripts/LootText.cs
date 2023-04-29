using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LootText : MonoBehaviour, IPointerDownHandler
{
    public TMP_Text itemNameTxt;
    public ItemObj itemObj;

    private void Start()
    {
        itemNameTxt.text = itemObj.item.name;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            Inventory.instance.MovePlayerToLoot(itemObj.gameObject);
        }
    }
}
