using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootButton : MonoBehaviour
{
    public Button btn;
    public TMP_Text itemNameTxt;
    public GameObject lootObj;

    private void Start()
    {
        btn.onClick.AddListener(OnClick);
        itemNameTxt.text = lootObj.GetComponent<Loot>().item.itemName;
    }

    public void OnClick()
    {
        PlayerControl.instance.StartPickUpLoot(lootObj);
    }
}
