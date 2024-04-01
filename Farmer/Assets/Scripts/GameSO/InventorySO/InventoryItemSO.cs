using Scripts.InventoryCode;
using System;
using UnityEngine;

namespace Scripts.SO.InventoryItem
{
    [CreateAssetMenu(fileName = "InventoryItemSO", menuName = "SO/InventoryItem")]
    public class InventoryItemSO : ScriptableObject, IInventoryItem
    {
        public string Name => _name;
        public Sprite Icon => _icon;
        public int Count => _count;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _count;

        public void RenderUI(InventoryCell inventoryCell)
        {
            inventoryCell.Icon.sprite = _icon;
            inventoryCell.Text.text = _name + $"({Count}";
        }
    }
}
