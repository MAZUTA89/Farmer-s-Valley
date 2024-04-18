using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory.Items
{
    public interface IProductionInventoryItem<T> : IInventoryItem
    {
        T ProductionObject { get; }
    }
}
