using Scripts.InventoryCode;
using System;
using System.Collections.Generic;

namespace Scripts.ItemUsage
{
    public class HoeItemHandler : ItemHandler
    {
        public override void HandleItem(IInventoryItem inventoryItem)
        {
            if(inventoryItem is IHoeInventoryItem)
            {

            }
            else
            {
                Successor.HandleItem(inventoryItem);
            }
        }
    }
}
