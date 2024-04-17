using Scripts.InventoryCode;
using Scripts.SaveLoader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Scripts.SaveLoader
{
    public class ChestData : PlacementItemData
    {
        public List<ItemData> Items;
        public ChestData()
        {
            Items = new List<ItemData>();
        }
        

        public void UpdateItems(List<InventoryItem> inventoryItems)
        {
            foreach (InventoryItem item in inventoryItems)
            {
                if(item is ToolItem)
                {
                    Items.Add(new ItemData() { SoName = item.Name });
                }
                if(item is QuantitativeItem)
                {
                    var quantitativeItem = (item as QuantitativeItem);
                    Items.Add(new QuantitativeItemData()
                    { SoName = quantitativeItem.Name, Count = quantitativeItem.Count });
                }
            }
        }
    }
}