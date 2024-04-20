using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using System;
using System.Collections.Generic;


namespace Scripts.InventoryCode
{
    public interface IChestInventoryItem : IProductionInventoryItem<Chest>
    {
        List<IInventoryItem> Items { get; set; }
    }
}
