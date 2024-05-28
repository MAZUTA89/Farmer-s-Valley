using System.Collections.Generic;

namespace Scripts.InventoryCode
{
    public interface IInventoryPanelFactory
    {
        public InventoryBase Create(List<InventoryItem> inventoryItems);
    }
}
