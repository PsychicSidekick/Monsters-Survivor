using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemButton : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text itemNameTxt;
    public Item item;

    public GameObject lootObjPrefab;

    void Start()
    {
        itemNameTxt.text = item.itemName;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            Debug.Log("Left Click");
        } 
        else if (eventData.pointerId == -2)
        {
            DropItem();
        }
    }

    public void DropItem()
    {
        Loot loot = Instantiate(lootObjPrefab, PlayerControl.instance.transform.position, Quaternion.identity).GetComponent<Loot>();
        loot.item = item;
        Inventory.instance.Remove(item);
    }
}
