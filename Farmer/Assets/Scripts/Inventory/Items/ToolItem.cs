using Scripts.SaveLoader;
using Scripts.SO.InventoryItem;
using System;
using System.Collections.Generic;


namespace Scripts.InventoryCode
{
    public class ToolItem : InventoryItem
    {
        public ToolItem(InventoryItemAssetData assetData) : base(assetData)
        {
        }

        public override ItemData GetItemData()
        {
            return new ItemData() { SoName = Name };
        }
    }
}
