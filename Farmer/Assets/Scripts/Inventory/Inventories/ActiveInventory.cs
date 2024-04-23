using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.ItemUsage;

namespace Scripts.InventoryCode
{
    public class ActiveInventory : InventoryStorage
    {
        InputService _inputService;
        int _chosenIndex;
        ItemApplier _applier;
        public IInventoryItem ChosenItem { get; private set; }


        [Inject]
        public void ConstructActive(InputService inputService, ItemApplier itemApplier,
            [Inject(Id = "ActiveInventoryInfo")] InventoryInfo inventoryInfo,
            IInventoryCellFactory inventoryCellFactory)
        {
            _inputService = inputService;
            _applier = itemApplier;
            base.ConstructStorage(inventoryInfo, inventoryCellFactory);
        }

        public override void ConstructStorage([Inject(Id = "ActiveInventoryInfo")] InventoryInfo inventoryInfo,
            IInventoryCellFactory inventoryCellFactory)
        {
        }
        protected override void Start()
        {
            _chosenIndex = -1;
        }
        private void Update()
        {
            if (_inputService.IsChosenCell(out _chosenIndex) &&
                _chosenIndex < CurrentSize)
            {
                SelectCellByIndex(_chosenIndex);
                
            }
            if(ChosenItem != null)
                _applier.ApplyItem(ChosenItem);
        }
        protected override void SaveInventory()
        {
            _gameDataState.UpdateActivePackInventory(InventoryItems);
        }
        protected override void OnEndDrag()
        {
            base.OnEndDrag();

            SelectFirstCell();
        }
        void SelectCellByIndex(int index)
        {
            ChosenItem = InventoryItems[index];
            ChosenItem.IsSelected = true;
            DeactivateOther(ChosenItem);
        }
        void SelectFirstCell()
        {
            if (InventoryItems.Count > 0)
            {
                ChosenItem = InventoryItems[0];
                ChosenItem.IsSelected = true;
                DeactivateOther(ChosenItem);
            }
        }
        void DeactivateOther(IInventoryItem selectedItem)
        {
            foreach (var item in InventoryItems)
            {
                if (selectedItem.Equals(item))
                    continue;
                item.IsSelected = false;
            }
        }
        public override void OnDragInto(InventoryCell inventoryCell)
        {
        }
    }
}
