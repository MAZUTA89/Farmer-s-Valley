using Scripts.InventoryCode;
using System;
using System.Collections.Generic;


namespace Scripts.SaveLoader
{
    internal static class SaveLoaderExtension
    {
        public static List<ItemData> ConvertItemsToItemsData(List<InventoryItem> inventoryItems)
        {
            List<ItemData> items = new List<ItemData>();
            foreach (var item in inventoryItems)
            {
                var itemData = item.GetItemData();
                items.Add(itemData);
            }
            return items;
        }
    } 
}
