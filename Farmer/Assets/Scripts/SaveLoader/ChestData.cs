using Scripts.InventoryCode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.SaveLoader
{
    public class ChestData : PlacementItemData
    {
        public List<InventoryItemData> Items;
        public ChestData()
        {
            Items = new List<InventoryItemData>();
            ItemTypeName = nameof(ChestData);
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