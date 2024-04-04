using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SO.Inventory
{
    [CreateAssetMenu(fileName = "InventoryInfo", menuName = "SO/InventoryInfo")]
    public class InventoryInfo : ScriptableObject
    {
        public int TotalSize;
    }
}
