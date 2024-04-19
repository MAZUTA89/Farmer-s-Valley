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
        public List<IItemData> PlacementObjectsDataList;
        public List<IItemData> ActivePackInventory;
        public List<IItemData> BackPackInventory;

        public GameDataState(string gameDataStateName)
        {
            ActivePackInventory = new List<IItemData>();
            BackPackInventory = new List<IItemData>();
            GameDataStateName = gameDataStateName;
        }
        public void UpdateActivePackInventory(List<IInventoryItem> inventoryItems)
        {
            List<IItemData> itemDataList = new List<IItemData>();
            foreach (var item in inventoryItems)
            {
                itemDataList.Add(item.GetItemData());
            }
            ActivePackInventory = itemDataList;
        }
        public void UpdateBackPackInventory(List<IInventoryItem> inventoryItems)
        {
            List<IItemData> itemDataList = new List<IItemData>();
            foreach (var item in inventoryItems)
            {
                itemDataList.Add(item.GetItemData());
            }
            BackPackInventory = itemDataList;
        }
        
        public void AddItemData(IItemData itemData)
        {
            PlacementObjectsDataList.Add(itemData);
        }
        public void RemoveItemData(IItemData itemData)
        {
            PlacementObjectsDataList.Remove(itemData);
        }
    }
}
