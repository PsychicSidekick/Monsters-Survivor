using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave
{
    // List of all available item bases
    public static List<ItemBase> itemBases = new List<ItemBase>();

    [Serializable]
    public struct StorageSave
    {
        public List<ItemSave> savedStashItems;
        public List<ItemSave> savedInventoryItems;
        public List<ItemSave> savedEquippedItems;
    }

    [Serializable]
    public struct ItemSave
    {
        public Vector2Int positionInStorage;
        // ID of opccupied item slot, -1 if item was not equipped
        public int occupiedItemSlotID;

        public string itemName;
        public int itemBaseID;
        public List<ItemModifierSave> itemMods;

        public ItemSave(Item item)
        {
            positionInStorage = item.occupiedCell.pos;
            occupiedItemSlotID = -1;

            itemName = item.name;
            itemBaseID = itemBases.IndexOf(item.itemBase);
            itemMods = new List<ItemModifierSave>();
            foreach (StatModifier itemModifier in item.itemModifiers)
            {
                itemMods.Add(new ItemModifierSave(itemModifier));
            }
        }

        public ItemSave(Item item, ItemSlot itemSlot)
        {
            positionInStorage = Vector2Int.zero;
            occupiedItemSlotID = PlayerStorage.instance.itemSlots.IndexOf(itemSlot);

            itemName = item.name;
            itemBaseID = itemBases.IndexOf(item.itemBase);
            itemMods = new List<ItemModifierSave>();
            foreach (StatModifier itemModifier in item.itemModifiers)
            {
                itemMods.Add(new ItemModifierSave(itemModifier));
            }
        }
    }

    [Serializable]
    public struct ItemModifierSave
    {
        public float itemModValue;
        public StatType itemModStatType;
        public ModifierType itemModType;

        public ItemModifierSave(StatModifier statModifier)
        {
            itemModValue = statModifier.value;
            itemModStatType = statModifier.statType;
            itemModType = statModifier.type;
        }
    }

    public GameSave()
    {
        // Find all available item bases in Resources
        itemBases = Resources.LoadAll<ItemBase>("ItemBases").ToList();
    }

    public void NewSave()
    {
        PlayerPrefs.SetFloat("HighScore", 0);

        StorageSave storageSave;
        storageSave.savedStashItems = new List<ItemSave>();
        storageSave.savedInventoryItems = new List<ItemSave>();
        storageSave.savedEquippedItems = new List<ItemSave>();

        string json = JsonUtility.ToJson(storageSave);
        File.WriteAllText(Application.streamingAssetsPath + "/Save/save.txt", json);

        PlayerStorage.instance.CleanStorage();
    }

    public void Save()
    {
        StorageSave storageSave;
        storageSave.savedStashItems = new List<ItemSave>();
        storageSave.savedInventoryItems = new List<ItemSave>();
        storageSave.savedEquippedItems = new List<ItemSave>();

        // Save stash items
        List<List<Cell>> stash = PlayerStorage.instance.stash;
        Vector2Int stashSize = PlayerStorage.instance.stashSize;

        for (int x = 0; x < stashSize.x; x++)
        {
            for (int y = 0; y < stashSize.y; y++)
            {
                Cell cell = stash[x][y];
                if (cell.childCells != null && cell.childCells.Count > 0)
                {
                    ItemSave itemSave = new ItemSave(cell.occupiedBy);
                    storageSave.savedStashItems.Add(itemSave);
                }
            }
        }

        // Save inventory items
        List<List<Cell>> inventory = PlayerStorage.instance.inventory;
        Vector2Int inventorySize = PlayerStorage.instance.inventorySize;

        for (int x = 0; x < inventorySize.x; x++)
        {
            for (int y = 0; y < inventorySize.y; y++)
            {
                Cell cell = inventory[x][y];
                if (cell.childCells != null && cell.childCells.Count > 0)
                {
                    ItemSave itemSave = new ItemSave(cell.occupiedBy);
                    storageSave.savedInventoryItems.Add(itemSave);
                }
            }
        }

        // Save equipped items
        List<ItemSlot> itemSlots = PlayerStorage.instance.itemSlots;

        foreach (ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.equippedItem != null)
            {
                ItemSave itemSave = new ItemSave(itemSlot.equippedItem, itemSlot);
                storageSave.savedEquippedItems.Add(itemSave);
            }
        }

        // Write storage save to Json
        string json = JsonUtility.ToJson(storageSave);
        File.WriteAllText(Application.streamingAssetsPath + "/Save/save.txt", json);
    }

    public void Load()
    {
        // Read and create storage save from Json
        string json = File.ReadAllText(Application.streamingAssetsPath + "/Save/save.txt");
        StorageSave storageSave = JsonUtility.FromJson<StorageSave>(json);

        // Load saved items into stash
        foreach (ItemSave itemSave in storageSave.savedStashItems)
        {
            Item item = LoadItem(itemSave);
            PlayerStorage.instance.PlaceItem(item, PlayerStorage.instance.stash[itemSave.positionInStorage.x][itemSave.positionInStorage.y]);
        }

        // Load saved items into inventory
        foreach (ItemSave itemSave in storageSave.savedInventoryItems)
        {
            Item item = LoadItem(itemSave);
            PlayerStorage.instance.PlaceItem(item, PlayerStorage.instance.inventory[itemSave.positionInStorage.x][itemSave.positionInStorage.y]);
        }

        // Load saved items into item slots
        foreach (ItemSave itemSave in storageSave.savedEquippedItems)
        {
            Item item = LoadItem(itemSave);
            PlayerStorage.instance.PlaceItemInItemSlot(item, PlayerStorage.instance.itemSlots[itemSave.occupiedItemSlotID]);
        }

        PlayerStorage.instance.descriptionPanel.SetActive(false);
    }

    public Item LoadItem(ItemSave itemSave)
    {
        List<StatModifier> itemMods = new List<StatModifier>();

        // Recreate item mods
        foreach (ItemModifierSave itemModSave in itemSave.itemMods)
        {
            itemMods.Add(new StatModifier(itemModSave.itemModStatType, itemModSave.itemModValue, itemModSave.itemModType));
        }

        Item item = new Item(itemBases[itemSave.itemBaseID], itemMods);
        PlayerStorage.instance.InstantiateItemImage(item);

        return item;
    }
}
