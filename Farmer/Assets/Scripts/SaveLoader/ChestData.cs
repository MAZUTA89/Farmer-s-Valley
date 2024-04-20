using Scripts.InventoryCode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.SaveLoader
{
    public class ChestData : PlacementItemData
    {
        public List<InventoryItemData> Items;
        public ChestData(PlacementItemData placementItemData)
        {
            Items = new List<InventoryItemData>();
            SetPosition(placementItemData.GetPosition());
        }
        

        public void UpdateItems(List<IInventoryItem> inventoryItems)
        {
            foreach (var item in inventoryItems)
            {
                Items.Add(item.GetItemData());
            }
        }
    }
}