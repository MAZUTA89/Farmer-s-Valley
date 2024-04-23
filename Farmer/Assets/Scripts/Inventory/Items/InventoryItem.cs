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
        public bool IsSelected { get; set; }
        public string Name => _name;

        public Sprite Icon => _icon;

        public Color Color => _color;

        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [ColorUsage(true)]
        [SerializeField] private Color _color;
        [SerializeField] protected bool IsCountTextActive;
        
        public virtual void RenderUI(InventoryCell inventoryCell)
        {
            inventoryCell.Icon.sprite = _icon;
            inventoryCell.Text.text = _name;
            inventoryCell.Text.color = _color;
            inventoryCell.CountText.gameObject.SetActive(IsCountTextActive);
            inventoryCell.SelectIcon.gameObject.SetActive(IsSelected);
        }
        
        public virtual InventoryItemData GetItemData()
        {
            InventoryItemData inventoryItemData
                = new InventoryItemData() { SoName = _name };
            return inventoryItemData;
        }
    }
}
