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
        protected List<InventoryItem> InventoryItems;
        protected IInventoryCellFactory _inventoryCellFactory;
        private Transform _globalVisualContext;
        

        protected Action<InventoryCell> OnBeginDragEvent;
        protected Action OnEndDragEvent;

        private GameObject _tmpEmptyCell;


        [Inject]
        public void Construct([Inject(Id = "DragParent")] Transform dragParent)
        {
            _globalVisualContext = dragParent;
        }

        public void Initialize(List<InventoryItemAssetData> inventoryItemAssetData)
        {
            InventoryItems = InitializeInventoryItems(inventoryItemAssetData);
            Initialize(InventoryItems);
        }
        public void Initialize(List<InventoryItem> inventoryItems)
        {
            OnStart();
            foreach (var item in inventoryItems)
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

        public void RegisterDragEvents(InventoryCell inventoryCell)
        {
            inventoryCell.RegisterEvents(OnEndDragEvent, OnBeginDragEvent);
        }
        public virtual void AddItem(InventoryItem inventoryItem)
        {
            InventoryCell newCell = _inventoryCellFactory.Create(Container);
            newCell.Initialize(_globalVisualContext, inventoryItem);
            RegisterDragEvents(newCell);
        }
        /// <summary>
        /// Возвращает true, если инвентарь заполнен
        /// </summary>
        /// <returns></returns>
        public bool IsFull()
        {
            if(CurrentSize >= TotalSize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        protected virtual void OnStart()
        {
            Container = transform;
            DragExtension.RegisterInventoryRectTransform
                (gameObject.GetComponent<RectTransform>());
            OnBeginDragEvent += OnBeginDragCell;
            OnEndDragEvent += OnEndDrag;
        }
        private void OnDisable()
        {
            OnBeginDragEvent -= OnBeginDragCell;
            OnEndDragEvent -= OnEndDrag;
        }
        protected virtual void OnBeginDragCell(InventoryCell inventoryCell)
        {
            _tmpEmptyCell = _inventoryCellFactory.CreateEmpty(inventoryCell);
            
        }
        protected virtual void OnEndDrag()
        {
            if(_tmpEmptyCell != null)
                Destroy(_tmpEmptyCell);
        }
    }
}
