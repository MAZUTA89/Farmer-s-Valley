using Scripts.InventoryCode;
using Scripts.PlacementCode;
using System;
using System.Collections.Generic;
using System.Numerics;


namespace Scripts.SaveLoader
{
    public class GameDataState
    {
        public string GameDataStateName;
        public PlayerData PlayerData;
        public List<IItemData> ItemDataList;
        public List<ChestData> Chests;
        public List<ItemData> ActivePackInventory;
        public List<ItemData> BackPackInventory;

        public GameDataState(string gameDataStateName)
        {
            Chests = new List<ChestData>();
            ActivePackInventory = new List<ItemData>();
            BackPackInventory = new List<ItemData>();
            GameDataStateName = gameDataStateName;
        }
        public void UpdateActivePackInventory(List<InventoryItem> inventoryItems)
        {
            ActivePackInventory =
                SaveLoaderExtension.ConvertItemsToItemsData(inventoryItems);
        }
        public void UpdateBackPackInventory(List<InventoryItem> inventoryItems)
        {
            BackPackInventory =
                SaveLoaderExtension.ConvertItemsToItemsData(inventoryItems);
        }
        
        public void AddItemData(IItemData itemData)
        {
            ItemDataList.Add(itemData);
        }
        public void RemoveItemData(IItemData itemData)
        {
            ItemDataList.Remove(itemData);
        }
    }
}
