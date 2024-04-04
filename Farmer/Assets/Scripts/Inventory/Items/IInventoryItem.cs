using System;
using UnityEngine;

namespace Scripts.InventoryCode
{
    public interface IInventoryItem
    {
        string Name { get; }
        Sprite Icon { get; }
        
    }
}
