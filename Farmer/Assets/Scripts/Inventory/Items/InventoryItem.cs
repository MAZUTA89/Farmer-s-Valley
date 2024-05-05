using Scripts.InventoryCode;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    public abstract class InventoryItem : ScriptableObject, IDataBaseItem
    {
        public bool IsSelected { get; set; }

        public string DisplayName = "";

        public Sprite Icon => _icon;

        public Color Color => _color;

        public string UniqueName => _name;

        public int MaxStackSize = 10;
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

        public abstract bool ApplyCondition(Vector3Int target);
        public abstract bool Apply(Vector3Int target);

        public virtual void RenderUI(InventoryCell inventoryCell)
        {
            inventoryCell.Icon.sprite = _icon;
            inventoryCell.NameDisplayText.text = DisplayName;
            inventoryCell.NameDisplayText.color = _color;
            inventoryCell.CountText.color = _color;
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
    }
}
