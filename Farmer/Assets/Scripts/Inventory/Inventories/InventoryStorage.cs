using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using Zenject;

namespace Scripts.InventoryCode
{
    public class InventoryStorage : InventoryBase
    {
        [Inject]
        public void ConstructStorage(
            [Inject(Id = "InventoryStorageInfo")] InventoryInfo inventoryInfo)
        {
            TotalSize = inventoryInfo.TotalSize;
        }
    }
}
