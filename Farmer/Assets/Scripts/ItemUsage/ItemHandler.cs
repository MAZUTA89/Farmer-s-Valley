using Assets.Scripts.ItemUsage;
using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.ItemUsage
{
    public class ItemHandler : IItemHandler
    {
        public IItemHandler Successor { set; get; }


        public virtual void HandleItem(IInventoryItem inventoryItem)
        {
            Successor.HandleItem(inventoryItem);
        }
    }
}
