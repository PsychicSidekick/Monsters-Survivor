using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave
{
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
        public int occupiedItemSlotID;
        public string itemName;
        public int itemBaseID;
        public List<ItemModifierSave> itemMods;

        public ItemSave(Item item)
        {
            if (item.occupiedCell != null)
            {
                positionInStorage = item.occupiedCell.pos;
                occupiedItemSlotID = -1;
            }
            else
            {
                positionInStorage = Vector2Int.zero;
                occupiedItemSlotID = PlayerStorage.instance.itemSlots.FindIndex(itemSlot => itemSlot.slotType == item.itemBase.type);
            }
            itemName = item.name;
            itemBaseID = itemBases.FindIndex(itemBase => itemBase == item.itemBase);
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
        itemBases = Resources.LoadAll<ItemBase>("ItemBases").ToList();
    }

    public void ClearSave()
    {
        StorageSave storageSave;
        storageSave.savedStashItems = new List<ItemSave>();
        storageSave.savedInventoryItems = new List<ItemSave>();
        storageSave.savedEquippedItems = new List<ItemSave>();

        string json = JsonUtility.ToJson(storageSave);
        File.WriteAllText(Application.streamingAssetsPath + "/Save/save.txt", json);
    }

    public void Save()
    {
        StorageSave storageSave;
        storageSave.savedStashItems = new List<ItemSave>();
        storageSave.savedInventoryItems = new List<ItemSave>();
        storageSave.savedEquippedItems = new List<ItemSave>();

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

        List<ItemSlot> itemSlots = PlayerStorage.instance.itemSlots;

        foreach (ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.equippedItem != null)
            {
                ItemSave itemSave = new ItemSave(itemSlot.equippedItem);
                storageSave.savedEquippedItems.Add(itemSave);
            }
        }

        string json = JsonUtility.ToJson(storageSave);
        File.WriteAllText(Application.streamingAssetsPath + "/Save/save.txt", json);
    }

    public void Load()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/Save/save.txt");
        StorageSave storageSave = JsonUtility.FromJson<StorageSave>(json);

        foreach (ItemSave itemSave in storageSave.savedStashItems)
        {
            Item item = LoadItem(itemSave);
            PlayerStorage.instance.PlaceItem(item, PlayerStorage.instance.stash[itemSave.positionInStorage.x][itemSave.positionInStorage.y]);
        }

        foreach (ItemSave itemSave in storageSave.savedInventoryItems)
        {
            Item item = LoadItem(itemSave);
            PlayerStorage.instance.PlaceItem(item, PlayerStorage.instance.inventory[itemSave.positionInStorage.x][itemSave.positionInStorage.y]);
        }

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

        foreach (ItemModifierSave itemModSave in itemSave.itemMods)
        {
            itemMods.Add(new StatModifier(itemModSave.itemModStatType, itemModSave.itemModValue, itemModSave.itemModType));
        }

        Item item = new Item(itemBases[itemSave.itemBaseID], itemMods);
        PlayerStorage.instance.InstantiateItemImage(item);

        return item;
    }
}
