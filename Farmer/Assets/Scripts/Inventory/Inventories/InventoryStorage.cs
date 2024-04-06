using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using Zenject;

namespace Scripts.InventoryCode
{
    public class InventoryStorage : InventoryBase
    {
        [Inject]
        public virtual void ConstructStorage(
            [Inject(Id = "InventoryStorageInfo")] InventoryInfo inventoryInfo,
            IInventoryCellFactory inventoryCellFactory)
        {
            TotalSize = inventoryInfo.TotalSize;
            _inventoryCellFactory = inventoryCellFactory;
        }
    }
}
