using Assets.Scripts.Inventory.Items;
using Scripts.InventoryCode;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "QuantitativeInventoryItem",
            menuName = "SO/InventoryItems/QuantitativeInventoryItem")]
    public class QuantitativeInventoryItem : InventoryItem, IQuantitativeInventoryItem
    {
        public int Count => _count;
        [SerializeField] private int _count;

        public override IItemData GetItemData()
        {
            IItemData itemData = base.GetItemData();
            QuantitativeItemData quantitativeItemData =
                new QuantitativeItemData((InventoryItemData)itemData)
                { Count = _count };
            return quantitativeItemData;
        }
    }
}
