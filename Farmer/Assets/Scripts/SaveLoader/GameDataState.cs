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
        public PlacementData PlacementData;
        

        public GameDataState(string gameDataStateName)
        {
            ActivePackInventory = new List<InventoryItemData>();
            BackPackInventory = new List<InventoryItemData>();
            GameDataStateName = gameDataStateName;
            PlacementObjectsDataList = new List<PlacementItemData>();
            PlacementData = new PlacementData();
        }
        public void UpdateActivePackInventory(List<InventoryItem> inventoryItems)
        {
            List<InventoryItemData> itemDataList =
                new List<InventoryItemData>();
            foreach (var item in inventoryItems)
            {
                itemDataList.Add(item.GetData());
            }
            ActivePackInventory = itemDataList;
        }
        public void UpdateBackPackInventory(List<InventoryItem> inventoryItems)
        {
            List<InventoryItemData> itemDataList =
                new List<InventoryItemData>();
            foreach (var item in inventoryItems)
            {
                itemDataList.Add(item.GetData());
            }
            BackPackInventory = itemDataList;
        }
        public void UpdatePlacementData(PlacementData placementData)
        {
            PlacementData = placementData;
        }
        public void AddItemData(PlacementItemData itemData)
         {
            if (PlacementObjectsDataList.Contains(itemData) == false)
            {
                PlacementObjectsDataList.Add(itemData);
            }
        }
        public void RemoveItemData(PlacementItemData itemData)
        {
            PlacementObjectsDataList.Remove(itemData);
        }
    }
}
