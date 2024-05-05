using Scripts.InventoryCode;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.ItemUsage
{
    public interface IItemHandler
    {
        IItemHandler Successor { get; set; }
        void HandleItem(InventoryItem inventoryItem);
    }
}
