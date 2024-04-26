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
        public int Count
        {
            get
            {
                return _count;
            }
            set 
            { 
                _count = value;
                if(_count < 0)
                    _count = 0;
            }
        }
        [SerializeField] private int _count;
        public override void RenderUI(InventoryCell inventoryCell)
        {
            base.RenderUI(inventoryCell);
            inventoryCell.CountText.text = _count.ToString();
        }
        public override InventoryItemData GetItemData()
        {
            InventoryItemData itemData = base.GetItemData();
            QuantitativeItemData quantitativeItemData =
                new QuantitativeItemData(itemData)
                { Count = _count };
            return quantitativeItemData;
        }
    }
}
