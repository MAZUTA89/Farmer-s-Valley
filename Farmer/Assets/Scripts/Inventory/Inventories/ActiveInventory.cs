using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace Scripts.InventoryCode
{
    public class ActiveInventory : InventoryStorage
    {
        public override void ConstructStorage([Inject(Id = "ActiveInventoryInfo")] InventoryInfo inventoryInfo, IInventoryCellFactory inventoryCellFactory)
        {
            base.ConstructStorage(inventoryInfo, inventoryCellFactory);
        }
        private void Update()
        {
            
        }
    }
}
