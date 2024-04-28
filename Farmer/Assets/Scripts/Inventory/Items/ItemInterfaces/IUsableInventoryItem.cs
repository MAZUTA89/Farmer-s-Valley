using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    public interface IUsableInventoryItem
    {
        UseConditionSO UseConditionSO { get; }
    }
}
