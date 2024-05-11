using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    public abstract class InventoryItem : ScriptableObject, IDataBaseItem, ICloneable
    {
        public bool IsSelected { get; set; }

        public string DisplayName = "";

        public Sprite Icon => _icon;

        public Color Color => _color;

        public string UniqueName => _name;

        public int MaxStackSize = 10;
        public int Count
        {
            get
            {
                return _startCount;
            }
            set
            {
                _startCount = value;

                if (_startCount < 0)
                    _startCount = 0;
            }
        }
        
        [SerializeField] private int _startCount;
        public bool Consumable = true;
        public int BuyPrice = -1;

        [SerializeField] private string _name;

        [SerializeField] private Sprite _icon;

        [ColorUsage(true)]
        [SerializeField] private Color _color;

        [SerializeField] protected bool IsCountTextActive;

        [Tooltip("Prefab that will be instantiated in the player hand when this is equipped")]
        public GameObject VisualPrefab;

        [Tooltip("Sound triggered when using the item")]
        public AudioClip[] UseSound;

        public string PlayerAnimatorTriggerUse = "GenericToolSwing";

        
        public virtual void InitializeCopy(InventoryItem inventoryItem)
        {
            DisplayName = inventoryItem.DisplayName;
            _icon = inventoryItem.Icon;
            _color = inventoryItem.Color;
            _name = inventoryItem.UniqueName;
            _startCount = inventoryItem.Count;
            MaxStackSize = inventoryItem.MaxStackSize;
            BuyPrice = inventoryItem.BuyPrice;
            Consumable = inventoryItem.Consumable;
            IsCountTextActive = inventoryItem.IsCountTextActive;
            VisualPrefab = inventoryItem.VisualPrefab;
            UseSound = inventoryItem.UseSound;
            PlayerAnimatorTriggerUse = inventoryItem.PlayerAnimatorTriggerUse;
        }
        public abstract bool ApplyCondition(Vector3Int target);

        public abstract bool Apply(Vector3Int target);
        
        public virtual void RenderUI(InventoryCell inventoryCell)
        {
            inventoryCell.Icon.sprite = _icon;
            inventoryCell.NameDisplayText.text = DisplayName;
            inventoryCell.NameDisplayText.color = _color;
            inventoryCell.CountText.color = _color;
            inventoryCell.CountText.text = Count.ToString();
            inventoryCell.CountText.gameObject.SetActive(IsCountTextActive);
            inventoryCell.SelectIcon.gameObject.SetActive(IsSelected);
        }
        
        public virtual InventoryItemData GetItemData()
        {
            InventoryItemData inventoryItemData
                = new InventoryItemData() { SoName = _name };
            return inventoryItemData;
        }
        public virtual bool NeedTarget()
        {
            return true;
        }

        public abstract object Clone();
        public virtual InventoryItemData GetData()
        {
            InventoryItemData inventoryItemData = new InventoryItemData()
            {
                SoName = UniqueName,
                Amount = Count
            };
            return inventoryItemData;
        }
        
    }
}
