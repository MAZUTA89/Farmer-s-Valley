using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    public interface IInventoryPanelFactory
    {
        public InventoryBase Create(List<InventoryItem> inventoryItems);
    }
}
