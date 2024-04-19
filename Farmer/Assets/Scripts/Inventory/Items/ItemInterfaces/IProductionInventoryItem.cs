using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    public interface IProductionInventoryItem<T> : IInventoryItem
    {
        T ProductionObject { get; }
    }
}
