using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.ItemUsage
{
    public interface IItemHandler
    {
        IItemHandler Successor { get; set; }
        void HandleItem(IInventoryItem inventoryItem);
    }
}
