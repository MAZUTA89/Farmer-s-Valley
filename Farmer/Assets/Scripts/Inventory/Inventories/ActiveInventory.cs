using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using Zenject;

namespace Scripts.InventoryCode
{
    public class ActiveInventory : InventoryStorage
    {

        //[Inject]
        //public void ConstructActive(
        //    [Inject(Id = "ActiveInventoryInfo")] InventoryInfo inventoryInfo
        //    )
        //{
        //    TotalSize = inventoryInfo.TotalSize;
        //}

        public override void ConstructStorage([Inject(Id = "ActiveInventoryInfo")] InventoryInfo inventoryInfo, IInventoryCellFactory inventoryCellFactory)
        {
            base.ConstructStorage(inventoryInfo, inventoryCellFactory);
        }
    }
}
