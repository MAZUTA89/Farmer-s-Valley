using Scripts.InventoryCode;
using System.Collections.Generic;


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
        

        public void UpdateItems(List<InventoryItem> inventoryItems)
        {
            foreach (var item in inventoryItems)
            {
                Items.Add(item.GetData());
            }
        }
    }
}