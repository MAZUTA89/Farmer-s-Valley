using Scripts.SO.InventoryItem;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Scripts.InventoryCode
{
    public abstract class InventoryBase : MonoBehaviour
    {
        protected int TotalSize;
        
        protected int CurrentSize => Container.childCount;
        public Transform Container { get; private set; }
        private Transform _globalVisualContext;
        private InventoryCell _inventoryCellTemplate;
        protected List<InventoryItem> InventoryItems;

        protected Action OnEndDragEvent;


        [Inject]
        public void Construct([Inject(Id = "DragParent")] Transform dragParent,
            [Inject(Id = "CellTemplate")] InventoryCell inventoryCellTemplate)
        {
            _globalVisualContext = dragParent;
            _inventoryCellTemplate = inventoryCellTemplate;
        }

        public void Initialize(List<InventoryItemAssetData> inventoryItemAssetData)
        {
            InventoryItems = InitializeInventoryItems(inventoryItemAssetData);
            Initialize(InventoryItems);
        }
        public void Initialize(List<InventoryItem> inventoryItems)
        {
            foreach (InventoryItem item in inventoryItems)
            {
                AddItem(item);
            }
        }
        List<InventoryItem> InitializeInventoryItems
            (List<InventoryItemAssetData> inventoryItemAssetData)
        {
            List<InventoryItem> inventoryItems = new List<InventoryItem>();
            foreach (var assetData in inventoryItemAssetData)
            {
                InventoryItem inventoryItem = null;
                switch (assetData)
                {
                    case QuantitativeItemAssetData quantitativeItem:
                        {
                            inventoryItem = new QuantitativeItem(quantitativeItem);
                            break;
                        }
                    case InventoryItemAssetData inventoryItemAssetDataBase:
                        {
                            inventoryItem = new ToolItem(inventoryItemAssetDataBase);
                            break;
                        }
                }
                inventoryItems.Add(inventoryItem);
            }
            return inventoryItems;
        }
        public virtual void AddItem(InventoryItem inventoryItem)
        {
            InventoryCell newCell = Instantiate(_inventoryCellTemplate, Container);
            newCell.Initialize(_globalVisualContext, inventoryItem, OnEndDragEvent);
        }
        public bool IsFull()
        {
            if(CurrentSize >= TotalSize)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected virtual void OnEndDrag()
        {

        }
        protected virtual void Start()
        {
            Container = transform;
            EndDragExtension.RegisterInventoryRectTransform
                (gameObject.GetComponent<RectTransform>());
            OnEndDragEvent += OnEndDrag;
        }
        private void OnDisable()
        {
            OnEndDragEvent -= OnEndDrag;
        }
    }
}
