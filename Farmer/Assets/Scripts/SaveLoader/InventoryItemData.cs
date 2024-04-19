using System;
using System.Collections.Generic;

namespace Scripts.SaveLoader
{
    public class InventoryItemData : IItemData
    {
        public string SoName;
    }
    public class QuantitativeItemData : InventoryItemData
    {
        public QuantitativeItemData(InventoryItemData inventoryItemData)
        {
           SoName = inventoryItemData.SoName;
        }
        public int Count;
    }
    public class BagInventoryItemData : QuantitativeItemData
    {
        public BagInventoryItemData(QuantitativeItemData inventoryItemData) : base(inventoryItemData)
        {
            Count = inventoryItemData.Count;
        }
    }
}
