using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory.Items
{
    public interface IQuantitativeInventoryItem : IInventoryItem
    {
        int Count { get; }
    }
}
