using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using System.Numerics;


namespace Scripts.SaveLoader
{
    public class GameDataState
    {
        public string GameDataStateName;
        public Vector2 PlayerPosition;
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
        public void AddChestData(ChestData chestData)
        {
            Chests.Add(chestData);
        }
        public void RemoveChestData(ChestData chestData)
        {
            Chests.Remove(chestData);
        }
    }
}
