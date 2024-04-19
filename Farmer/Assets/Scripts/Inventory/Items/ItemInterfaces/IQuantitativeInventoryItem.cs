using Scripts.InventoryCode;
using System;

namespace Scripts.InventoryCode
{
    public interface IQuantitativeInventoryItem : IInventoryItem
    {
        int Count { get; }
    }
}
