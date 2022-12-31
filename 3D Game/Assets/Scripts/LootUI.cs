using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootUI : MonoBehaviour
{
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log("Clicked on loot");
    }
}
