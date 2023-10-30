using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescriptionPanel : MonoBehaviour
{
    public Item item;
    public TMP_Text itemNameTxt;
    public TMP_Text itemModsTxt;

    private void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        transform.position = new Vector3(FindXPos(), FindYPos(), 0);
    }

    private float FindXPos()
    {
        float panelWidth = GetComponent<RectTransform>().rect.width;

        float descriptionHorizontalOffset = item.itemImage.GetComponent<RectTransform>().rect.width / 2;

        descriptionHorizontalOffset += panelWidth / 2;
        descriptionHorizontalOffset *= -1;
        
        return item.itemImage.transform.position.x + descriptionHorizontalOffset;
    }

    private float FindYPos()
    {
        float panelHeight = GetComponent<RectTransform>().rect.height;

        float minValue = panelHeight / 2;
        float maxValue = transform.parent.GetComponent<RectTransform>().rect.height - panelHeight / 2;

        return Mathf.Clamp(item.itemImage.transform.position.y, minValue, maxValue);
    }

    public void UpdateDescription(Item item)
    {
        this.item = item;
        itemNameTxt.text = item.name;

        string itemModString = null;

        foreach (StatModifier mod in item.itemModifiers)
        {
            itemModString += mod.modString + System.Environment.NewLine;
        }

        itemModsTxt.text = itemModString;
    }
}
