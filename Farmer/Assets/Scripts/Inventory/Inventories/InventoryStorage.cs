using Scripts.FarmGameEvents;
using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Zenject;
using UnityEngine;

namespace Scripts.InventoryCode
{
    public class InventoryStorage : InventoryBase, IPointerEnterHandler, IPointerExitHandler
    {
        public static bool IsMouseStay {  get; private set; }
        [Inject]
        public virtual void ConstructStorage(
            [Inject(Id = "InventoryStorageInfo")] InventoryInfo inventoryInfo,
            IInventoryCellFactory inventoryCellFactory)
        {
            TotalSize = inventoryInfo.TotalSize;
            _inventoryCellFactory = inventoryCellFactory;
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            GameEvents.OnExitTheGameEvent += OnExitTheGame;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            GameEvents.OnExitTheGameEvent -= OnExitTheGame;
        }
        protected override void SaveInventory()
        {
            _gameDataState.UpdateBackPackInventory(InventoryItems);
        }
        public virtual void OnExitTheGame()
        {
            SaveInventory();
        }
        protected override void OnEndDrag()
        {
            base.OnEndDrag();
        }
        public override void OnDragInto(InventoryCell inventoryCell)
        {
            base.OnDragInto(inventoryCell);
            inventoryCell.InventoryItem.IsSelected = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log(" OnPointerEnter");
             IsMouseStay = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log(" OnPointerExit");
            IsMouseStay = false;
        }
        public virtual void Update()
        {
            
        }
    }
}
