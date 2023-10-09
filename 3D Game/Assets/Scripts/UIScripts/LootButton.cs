using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LootButton : MonoBehaviour, IPointerDownHandler
{
    public TMP_Text itemNameTxt;
    public LootGameObject lootGameObject;

    private void Start()
    {
        itemNameTxt.text = lootGameObject.item.name;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            PlayerStorage.instance.MovePlayerToLoot(lootGameObject);
        }
    }
}
