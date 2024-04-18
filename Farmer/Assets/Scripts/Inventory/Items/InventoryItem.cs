using Scripts.InventoryCode;
using Scripts.SaveLoader;
using Scripts.SO.InventoryItem;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    public abstract class InventoryItem : ScriptableObject, IInventoryItem
    {
        public string Name => _name;

        public Sprite Icon => _icon;

        public ItemType ItemType => _itemType;

        private string _name;
        private Sprite _icon;
        private ItemType _itemType;
        
        public InventoryItem(IInventoryItem inventoryItem)
        {
            _itemType = inventoryItem.ItemType;
            _name = inventoryItem.Name;
            _icon = inventoryItem.Icon;
        }
       
        public virtual void RenderUI(InventoryCell inventoryCell)
        {
            inventoryCell.Icon.sprite = _icon;
            inventoryCell.Text.text = _name;
        }

        public abstract ItemData GetItemData();
        
    }

    
}
