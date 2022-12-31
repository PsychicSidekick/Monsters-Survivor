using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerDownHandler
{
    public string itemName;
    public Vector2 size;
    public bool equipped;
    public bool pickedUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.pointerId == -1)
        {
            Debug.Log("Left Click");
        }
    }
}
