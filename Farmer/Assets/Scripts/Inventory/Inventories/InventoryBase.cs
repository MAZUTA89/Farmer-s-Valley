﻿using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;


namespace Scripts.InventoryCode
{
    public abstract class InventoryBase : MonoBehaviour, IDragHandler
    {
        [SerializeField] protected Transform ContainerField;
        public Transform Container
        {
            get { return ContainerField; }
            private set { ContainerField = value; }
        }
        protected int TotalSize;
        protected int CurrentSize => Container.childCount;
        protected List<IInventoryItem> InventoryItems;
        protected IInventoryCellFactory _inventoryCellFactory;
        private Transform _globalVisualContext;
        private RectTransform _contextRect;


        protected Action<InventoryCell> OnBeginDragEvent;
        protected Action OnEndDragEvent;

        private GameObject _tmpEmptyCell;
        protected GameDataState _gameDataState;


        [Inject]
        public void Construct([Inject(Id = "DragParent")] Transform dragParent, 
            GameDataState gameDataState)
        {
            _globalVisualContext = dragParent;
            _gameDataState = gameDataState;
        }

        public void Initialize(List<IInventoryItem> inventoryItems)
        {
            InventoryItems = new List<IInventoryItem>(TotalSize);
            InventoryItems.InsertRange(0, inventoryItems);
            foreach (var item in inventoryItems)
            {
                AddItem(item);
            }

        }

        public void RegisterDragEvents(InventoryCell inventoryCell)
        {
            inventoryCell.RegisterEvents(OnEndDragEvent, OnBeginDragEvent);
        }
        public virtual void AddItem(IInventoryItem inventoryItem)
        {
            InventoryItems.Add(inventoryItem);
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
            if (CurrentSize >= TotalSize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Awake()
        {
            _contextRect = gameObject.GetComponent<RectTransform>();
        }
        protected virtual void Start()
        {
        }
        protected virtual void OnEnable()
        {
            DragExtension.RegisterInventoryRectTransform
                (_contextRect);
            OnBeginDragEvent += OnBeginDragCell;
            OnEndDragEvent += OnEndDrag;
        }
        protected virtual void OnDisable()
        {
            DragExtension.UnregisterInventoryRectTransform(_contextRect);
            OnBeginDragEvent -= OnBeginDragCell;
            OnEndDragEvent -= OnEndDrag;
        }
        protected virtual void OnBeginDragCell(InventoryCell inventoryCell)
        {
            _tmpEmptyCell = _inventoryCellFactory.CreateEmpty(inventoryCell);

        }
        protected virtual void OnEndDrag()
        {
            if (_tmpEmptyCell != null)
                Destroy(_tmpEmptyCell);
            InventoryItems = OverwriteInventoryItemsSequence();
        }

        public virtual void OnDrag(PointerEventData eventData)
        {

        }

        List<IInventoryItem> OverwriteInventoryItemsSequence()
        {
            List<IInventoryItem> items = new List<IInventoryItem>();
            for (int i = 0; i < Container.childCount; i++)
            {
                var cellObject = Container.GetChild(i);
                InventoryCell inventoryCell;
                if (cellObject.TryGetComponent(out inventoryCell))
                {
                    items.Add(inventoryCell.InventoryItem);
                }
            }
            return items;
        }
        public List<IInventoryItem> GetItems()
        {
            return InventoryItems;
        }
        
        protected virtual void SaveInventory()
        {

        }
    }
}