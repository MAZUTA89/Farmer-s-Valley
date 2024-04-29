using Scripts.InventoryCode;
using System;
using System.Collections.Generic;

namespace Scripts.SaveLoader
{
    public class InventoryItemData
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
    public class OakBagInventoryItemData : BagInventoryItemData
    {
        public OakBagInventoryItemData(BagInventoryItemData bagInventoryItemData)
            : base(bagInventoryItemData)
        {
            Count= bagInventoryItemData.Count;
        }
    }
    public class ChestInventoryItemData : QuantitativeItemData
    {
        public List<InventoryItemData> ItemsList;
        public ChestInventoryItemData(QuantitativeItemData inventoryItemData,
            List<InventoryItemData> inventoryItems)
            : base(inventoryItemData)
        {
            Count = inventoryItemData.Count;
            ItemsList = inventoryItems;
        }
    }
    public class HoeInventoryItemData : InventoryItemData
    {

    }
}
