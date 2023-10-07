using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGameOnClick()
    {
        SceneManager.LoadScene("Main");
    }

    public void ShowStashOnClick()
    {
        GameObject inventoryPanel = Inventory.instance.transform.GetChild(0).gameObject;
        inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
    }
}
