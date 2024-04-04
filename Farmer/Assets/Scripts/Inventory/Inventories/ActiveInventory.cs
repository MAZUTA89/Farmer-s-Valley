using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using Zenject;

namespace Scripts.InventoryCode
{
    public class ActiveInventory : InventoryBase
    {

        [Inject]
        public void ConstructActive(
            [Inject(Id = "ActiveInventoryInfo")] InventoryInfo inventoryInfo)
        {
            TotalSize = inventoryInfo.TotalSize;
        }
    }
}
