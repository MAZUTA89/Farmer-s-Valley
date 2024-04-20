using Scripts.InventoryCode;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "InventoryItem",
            menuName = "SO/InventoryItems/InventoryItem")]
    public class InventoryItem : ScriptableObject, IInventoryItem
    {
        public string Name => _name;

        public Sprite Icon => _icon;

        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
       
        public virtual void RenderUI(InventoryCell inventoryCell)
        {
            inventoryCell.Icon.sprite = _icon;
            inventoryCell.Text.text = _name;
        }

        public virtual InventoryItemData GetItemData()
        {
            InventoryItemData inventoryItemData
                = new InventoryItemData() { SoName = _name };
            return inventoryItemData;
        }
    }

    
}
