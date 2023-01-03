using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public List<GameObject> itemBtns = new List<GameObject>();

    public static Inventory instance;

    public GameObject inventoryUI;

    public TMP_Text label;

    public Button itemBtnPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryUI.SetActive(!inventoryUI.activeInHierarchy);
        }
    }

    public void AddItem(Item item)
    {
        inventory.Add(item);
        UpdateUI();
    }

    public void Remove(Item item)
    {
        inventory.Remove(item);
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (GameObject itemBtn in itemBtns)
        {
            Destroy(itemBtn);
        }

        itemBtns.Clear();

        for (int i = 0; i < inventory.Count; i++)
        {
            Button iBtn = Instantiate(itemBtnPrefab, label.transform.position + new Vector3(0, -40 * (i + 1), 0), Quaternion.identity);
            iBtn.GetComponent<ItemButton>().item = inventory[i];
            itemBtns.Add(iBtn.gameObject);
            iBtn.transform.SetParent(inventoryUI.transform);

        }
    }
}