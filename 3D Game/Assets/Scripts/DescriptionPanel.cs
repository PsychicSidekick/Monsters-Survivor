using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescriptionPanel : MonoBehaviour
{
    public ItemObj itemObj;

    public TMP_Text itemNameTxt;
    public TMP_Text itemBaseModTxt;
    public TMP_Text itemModTxt;

    private Rect itemImgRect;
    private Rect rect;

    private void Start()
    {
        itemNameTxt.text = itemObj.item.name;

        List<StatModifier> mods = itemObj.item.itemModifiers;
        itemBaseModTxt.text = mods[0].modString;

        string itemModString = null;

        foreach (StatModifier mod in itemObj.item.itemModifiers)
        {
            if (mod.modString == itemBaseModTxt.text)
            {
                continue;
            }
            itemModString += mod.modString + System.Environment.NewLine;
        }

        itemModTxt.text = itemModString;
        itemImgRect = itemObj.itemImg.GetComponent<RectTransform>().rect;
        rect = GetComponent<RectTransform>().rect;
    }

    private void Update()
    {
        Vector3 positionOffSet;

        if (itemObj.isEquipped)
        {
            positionOffSet = new Vector3(-itemImgRect.width / 2 - rect.width / 2, 0, 0);
        }
        else
        {
            positionOffSet = new Vector3(0, itemImgRect.height / 2 + rect.height / 2, 0);
        }

        transform.position = itemObj.itemImg.transform.position + positionOffSet;
    }
}
