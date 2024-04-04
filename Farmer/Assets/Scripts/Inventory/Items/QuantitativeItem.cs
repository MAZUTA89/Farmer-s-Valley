using Scripts.InventoryCode;
using Scripts.SO.InventoryItem;
using System;
using System.Collections.Generic;


namespace Scripts.InventoryCode
{
    public class QuantitativeItem : InventoryItem
    {
        public int Count { get; private set; }
        public QuantitativeItem(QuantitativeItemAssetData assetData) : base(assetData)
        {
            Count = assetData.Count;
        }

        public override void RenderUI(InventoryCell inventoryCell)
        {
            base.RenderUI(inventoryCell);
            inventoryCell.name += $"{Count}";
        }
    }
}
