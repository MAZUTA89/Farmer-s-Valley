using HappyHarvest;
using Scripts.Inventory;
using Scripts.MouseHandle;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;


namespace Scripts.InventoryCode
{
    public abstract class InventoryBase : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] protected Transform ContainerField;
        public Transform Container
        {
            get { return ContainerField; }
            private set { ContainerField = value; }
        }
        protected int TotalSize;
        protected int CurrentSize => InventoryItems.Count;
        protected List<InventoryItem> InventoryItems;
        protected IInventoryCellFactory _inventoryCellFactory;
        private Transform _globalVisualContext;
        private RectTransform _contextRect;


        protected Action<InventoryCell> OnBeginDragEvent;
        protected Action OnEndDragEvent;

        private GameObject _tmpEmptyCell;
        MouseCursor _mouseCursor;

        [Inject]
        public void Construct([Inject(Id = "DragParent")] Transform dragParent,
           MouseCursor mouseCursor)
        {
            _globalVisualContext = dragParent;
            _mouseCursor = mouseCursor;
        }
        private void Awake()
        {
            _contextRect = gameObject.GetComponent<RectTransform>();

        }
        protected virtual void OnEnable()
        {
            DragExtension.RegisterInventoryRectTransform
                (_contextRect);
            
        }
        protected virtual void OnDisable()
        {
            DragExtension.UnregisterInventoryRectTransform(_contextRect);
        }
        private void OnDestroy()
        {
            OnBeginDragEvent -= OnBeginDragCell;
            OnEndDragEvent -= OnEndDrag;
        }
        protected virtual void Start()
        {
        }
        public void Initialize(List<InventoryItem> inventoryItems)
        {
            OnBeginDragEvent += OnBeginDragCell;
            OnEndDragEvent += OnEndDrag;
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

        public bool AddItem(InventoryItem newItem)
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

            
            return remainingToFit == 0;
        }
        public void RemoveItem(int itemIndex) 
        {
            for (int i = 0; i < Container.childCount; i++)
            {
                if(i == itemIndex)
                {
                    Transform cell = Container.GetChild(i);
                    Destroy(cell.gameObject);
                }
            }
        }
        void CreateCellForItem(InventoryItem inventoryItem)
        {
            InventoryCell newCell = _inventoryCellFactory.Create(Container);
            newCell.Initialize(_globalVisualContext, inventoryItem);
            RegisterDragEvents(newCell);
        }
        /// <summary>
        /// Возвращает false, если инвентарь заполнен
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
        public List<ItemContextData> GetItemsContextData()
        {
            List<ItemContextData> itemContextDatas = new List<ItemContextData>();

            for (int i = 0; i < Container.childCount; i++)
            {
                var name = Container.name;
                var index = i;
                if(Container.GetChild(index).TryGetComponent(out InventoryCell cell))
                {
                    ItemContextData itemContextData = new ItemContextData(
                        cell.InventoryItem, index, name);

                    itemContextDatas.Add(itemContextData);
                }
            }
            return itemContextDatas;
        }

        
        public virtual void OnDragInto(InventoryCell inventoryCell)
        {

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _mouseCursor.ChangeCursor(CursorType.Drag);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _mouseCursor.ChangeCursor(CursorType.Default);
        }
    }
}
