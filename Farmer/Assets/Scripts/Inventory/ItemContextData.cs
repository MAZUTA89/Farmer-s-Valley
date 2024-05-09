using Scripts.InventoryCode;
using System;
using System.Collections.Generic;

namespace Scripts.Inventory
{
    public class ItemContextData
    {
        public InventoryItem Item { get; private set; }
        public int Index { get; private set; }
        public string ContextName { get; private set; }
        public ItemContextData(InventoryItem inventoryItem,
            int index, string Name)
        {
            Item = inventoryItem;
            Index = index;
            ContextName = Name;
        }
    }
}
