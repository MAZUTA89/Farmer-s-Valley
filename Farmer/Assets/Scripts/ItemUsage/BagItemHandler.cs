using Scripts.InventoryCode;
using System;
using System.Collections.Generic;


namespace Scripts.ItemUsage
{
    public class BagItemHandler : ItemHandler
    {
        public override void HandleItem(IInventoryItem inventoryItem)
        {
            if(inventoryItem is IBagInventoryItem)
            {

            }
            else
            {
                Successor.HandleItem(inventoryItem);
            }
        }
    }
}
