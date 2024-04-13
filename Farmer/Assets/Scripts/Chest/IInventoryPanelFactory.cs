using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.ChestItem
{
    public interface IInventoryPanelFactory
    {
        public InventoryBase Create(List<InventoryItem> inventoryItems);
    }
}
