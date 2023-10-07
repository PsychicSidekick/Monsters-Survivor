using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescriptionPanel : MonoBehaviour
{
    public Item item;

    public TMP_Text itemNameTxt;
    public TMP_Text itemModTxt;

    private Rect itemImgRect;
    private Rect rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>().rect;
    }

    private void Update()
    {
        Vector3 positionOffSet;

        if (item.isEquipped)
        {
            positionOffSet = new Vector3(-itemImgRect.width / 2 - rect.width / 2, 0, 0);
        }
        else
        {
            positionOffSet = new Vector3(0, itemImgRect.height / 2 + rect.height / 2, 0);
        }

        transform.position = item.itemImage.transform.position + positionOffSet;
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

        itemModTxt.text = itemModString;
        itemImgRect = item.itemImage.GetComponent<RectTransform>().rect;
    }
}
