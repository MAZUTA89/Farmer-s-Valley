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
        private GameObject _emptyCellTemplate;
        protected List<InventoryItem> InventoryItems;

        protected Action<InventoryCell> OnBeginDragEvent;
        protected Action OnEndDragEvent;

        private GameObject _tmpEmptyCell;


        [Inject]
        public void Construct([Inject(Id = "DragParent")] Transform dragParent,
            [Inject(Id = "CellTemplate")] InventoryCell inventoryCellTemplate,
            [Inject(Id = "EmptyCellTemplate")] GameObject emptyInventoryCellTemplate)
        {
            _globalVisualContext = dragParent;
            _inventoryCellTemplate = inventoryCellTemplate;
            _emptyCellTemplate = emptyInventoryCellTemplate;
        }

        public void Initialize(List<InventoryItemAssetData> inventoryItemAssetData)
        {
            InventoryItems = InitializeInventoryItems(inventoryItemAssetData);
            Initialize(InventoryItems);
        }
        public void Initialize(List<InventoryItem> inventoryItems)
        {
            OnStart();
            for (int i = 0; i < TotalSize; i++)
            {
                if(i < inventoryItems.Count)
                {
                    AddItem(inventoryItems[i]);
                }
                else
                {
                    Instantiate(_emptyCellTemplate, Container);
                    //AddItem(null);
                }
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
            newCell.Initialize(_globalVisualContext, inventoryItem,
                OnEndDragEvent, OnBeginDragEvent);
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
            //Debug.Log("Invoke begin drag!");
            int siblingIndex = inventoryCell.transform.GetSiblingIndex();
            _tmpEmptyCell = Instantiate(_emptyCellTemplate, Container);
            _tmpEmptyCell.name = "SiblingCell";
            _tmpEmptyCell.transform.SetSiblingIndex(siblingIndex);
        }
        protected virtual void OnEndDrag()
        {
            Destroy(_tmpEmptyCell);
        }

        
    }
}
