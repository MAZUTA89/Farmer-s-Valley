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
        public List<PlacementItemData> PlacementObjectsDataList;
        public List<InventoryItemData> ActivePackInventory;
        public List<InventoryItemData> BackPackInventory;

        public GameDataState(string gameDataStateName)
        {
            ActivePackInventory = new List<InventoryItemData>();
            BackPackInventory = new List<InventoryItemData>();
            GameDataStateName = gameDataStateName;
            PlacementObjectsDataList = new List<PlacementItemData>();
        }
        public void UpdateActivePackInventory(List<IInventoryItem> inventoryItems)
        {
            List<InventoryItemData> itemDataList =
                new List<InventoryItemData>();
            foreach (var item in inventoryItems)
            {
                itemDataList.Add(item.GetItemData());
            }
            ActivePackInventory = itemDataList;
        }
        public void UpdateBackPackInventory(List<IInventoryItem> inventoryItems)
        {
            List<InventoryItemData> itemDataList = 
                new List<InventoryItemData>();
            foreach (var item in inventoryItems)
            {
                itemDataList.Add(item.GetItemData());
            }
            BackPackInventory = itemDataList;
        }
        public void AddItemData(PlacementItemData itemData)
        {
            PlacementObjectsDataList.Add(itemData);
        }
        public void RemoveItemData(PlacementItemData itemData)
        {
            PlacementObjectsDataList.Remove(itemData);
        }
    }
}
