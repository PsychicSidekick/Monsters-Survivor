using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteItemButton : MonoBehaviour
{
    public void OnClick()
    {
        if (PlayerStorage.instance.cursorItem != null)
        {
            Destroy(PlayerStorage.instance.cursorItem.itemImage);
            PlayerStorage.instance.cursorItem = null;
        }
    }
}
