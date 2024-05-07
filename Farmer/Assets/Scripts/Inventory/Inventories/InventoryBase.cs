using HappyHarvest;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using static UnityEditor.Progress;


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
        protected List<InventoryItem> InventoryItems;
        protected IInventoryCellFactory _inventoryCellFactory;
        private Transform _globalVisualContext;
        private RectTransform _contextRect;


        protected Action<InventoryCell> OnBeginDragEvent;
        protected Action OnEndDragEvent;

        private GameObject _tmpEmptyCell;
        protected GameDataState _gameDataState;


        [Inject]
        public void Construct([Inject(Id = "DragParent")] Transform dragParent,
            GameDataState gameDataState
           )
        {
            _globalVisualContext = dragParent;
            _gameDataState = gameDataState;
        }

        public void Initialize(List<InventoryItem> inventoryItems)
        {
            InventoryItems = new List<InventoryItem>(TotalSize);
            InventoryItems.InsertRange(0, inventoryItems);
            foreach (var item in inventoryItems)
            {
                CreateCellForItem(item);
            }
            foreach (var item in inventoryItems)
            {
                item.IsSelected = false;
            }
        }

        public void RegisterDragEvents(InventoryCell inventoryCell)
        {
            inventoryCell.RegisterEvents(OnEndDragEvent, OnBeginDragEvent);
        }

        public bool AddItem(InventoryItem newItem, int amount = 1)
        {
            InventoryItem newItemCopy = (InventoryItem)newItem.Clone();
            int remainingToFit = newItemCopy.Count;

            //first we check if there is already that item in the inventory
            for (int i = 0; i < InventoryItems.Count; ++i)
            {
                if (InventoryItems[i].UniqueName == newItemCopy.UniqueName
                    && InventoryItems[i].Count < newItemCopy.MaxStackSize)
                {
                    int fit = Mathf.Min(newItemCopy.MaxStackSize
                        - InventoryItems[i].Count,
                        remainingToFit);
                    InventoryItems[i].Count += fit;
                    remainingToFit -= fit;

                    if (remainingToFit == 0)
                        return true;
                }
            }

            
            newItemCopy.Count = remainingToFit;
            CreateCellForItem(newItemCopy);
            InventoryItems = OverwriteInventoryItemsSequence();

            //if we reach here we couldn't fit it in existing stack, so we look for an empty place to fit it
            //for (int i = 0; i < TotalSize; ++i)
            //{
            //    if (InventoryItems[i] == null)
            //    {
            //        InventoryItems[i] = newItem;
            //        int fit = Mathf.Min(newItem.MaxStackSize -
            //            InventoryItems[i].Count, remainingToFit);
            //        remainingToFit -= fit;
            //        InventoryItems[i].Count = fit;
            //        if (remainingToFit == 0)
            //            return true;
            //    }
            //}

            //we couldn't had so no space left
            return remainingToFit == 0;

            //if (newItem.Consumable)
            //{
            //    int remainingFit = newItem.Count;
            //    for (int i = 0; i < InventoryItems.Count; i++)
            //    {
            //        var item = InventoryItems[i];
            //        if (item.UniqueName == newItem.UniqueName && item.Consumable &&
            //            item.Count < item.MaxStackSize)
            //        {
            //            int itemRemainingFit = item.MaxStackSize - item.Count;
            //            int fit = Mathf.Min(itemRemainingFit, remainingFit);
            //            item.Count += fit;
            //            remainingFit -= fit;
            //            if (remainingFit == 0)
            //            {
            //                return true;
            //            }
            //        }

            //    }
            //    newItem.Count = remainingFit;
            //    if (IsFull())
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        CreateCellForItem(newItem);
            //        return true;
            //    }
            //}
            //else
            //{
            //    CreateCellForItem(newItem);
            //    return true;
            //}
        }
        void CreateCellForItem(InventoryItem inventoryItem)
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
            if (CurrentSize >= TotalSize)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CanFit(InventoryItem newItem)
        {
            int remainingToFit = newItem.Count;
            for (int i = 0; i < InventoryItems.Count; i++)
            {
                var item = InventoryItems[i];

                if (item.UniqueName == newItem.UniqueName &&
                    item.Count < newItem.MaxStackSize)
                {
                    int fit = newItem.MaxStackSize - item.Count;
                    item.Count += fit;
                    remainingToFit -= fit;

                    if (remainingToFit == 0)
                        return true;
                }
            }
            return false;

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
            Debug.Log(gameObject.name);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {

        }

        List<InventoryItem> OverwriteInventoryItemsSequence()
        {
            List<InventoryItem> items = new List<InventoryItem>(TotalSize);
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
        public List<InventoryItem> GetItems()
        {
            return InventoryItems;
        }

        protected virtual void SaveInventory()
        {

        }
        public virtual void OnDragInto(InventoryCell inventoryCell)
        {

        }
    }
}
