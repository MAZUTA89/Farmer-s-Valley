using Scripts.InventoryCode;
using System;
using UnityEngine;

namespace Scripts.SO.InventoryItem
{
    [CreateAssetMenu(fileName = "InventoryItemSO", 
        menuName = "SO/InventoryItems/InventoryItem")]
    public class InventoryItemAssetData : ScriptableObject, IInventoryItem
    {
        public string Name => _name;
        public Sprite Icon => _icon;
        public ItemType ItemType => _itemType;

        [SerializeField] private string _name;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemType _itemType;
       
    }
}
