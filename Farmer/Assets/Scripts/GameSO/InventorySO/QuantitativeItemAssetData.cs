using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.SO.InventoryItem
{
    [CreateAssetMenu(fileName = "QuantitativeItem", menuName = "SO/QuantitativeItem")]
    public class QuantitativeItemAssetData : InventoryItemAssetData
    {
        public int Count => _count;
        [SerializeField] private int _count;
    }
}
