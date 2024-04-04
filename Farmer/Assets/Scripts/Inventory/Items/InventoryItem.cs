using Scripts.InventoryCode;
using Scripts.SO.InventoryItem;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    public abstract class InventoryItem : IInventoryItem
    {
        public string Name => _name;

        public Sprite Icon => _icon;

        private string _name;
        private Sprite _icon;

        public InventoryItem(InventoryItemAssetData assetData)
        {
                
            _name = assetData.Name;
            _icon = assetData.Icon;
        }

        public virtual void RenderUI(InventoryCell inventoryCell)
        {
            inventoryCell.Icon.sprite = _icon;
            inventoryCell.Text.text = _name;
        }
    }
}
