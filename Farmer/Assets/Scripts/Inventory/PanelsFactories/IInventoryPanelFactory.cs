using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    public interface IInventoryPanelFactory
    {
        public InventoryBase Create(List<IInventoryItem> inventoryItems);
    }
}
