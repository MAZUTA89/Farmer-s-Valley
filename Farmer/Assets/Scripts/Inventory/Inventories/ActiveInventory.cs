using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.ItemUsage;
using Scripts.PlacementCode;
using Scripts.PlayerCode;

namespace Scripts.InventoryCode
{
    public class ActiveInventory : InventoryStorage
    {
        InputService _inputService;
        int _chosenIndex;
        Player _player;
        ItemApplier _applier;
        public InventoryItem ChosenItem { get; private set; }
        MarkerController _markerController;

        [Inject]
        public void ConstructActive(InputService inputService, ItemApplier itemApplier,
            [Inject(Id = "ActiveInventoryInfo")] InventoryInfo inventoryInfo,
            MarkerController markerController,
            Player player,
            IInventoryCellFactory inventoryCellFactory)
        {
            _markerController = markerController;
            _inputService = inputService;
            _applier = itemApplier;
            _player = player;
            base.ConstructStorage(inventoryInfo, inventoryCellFactory);
        }

        public override void ConstructStorage([Inject(Id = "ActiveInventoryInfo")] InventoryInfo inventoryInfo,
            IInventoryCellFactory inventoryCellFactory)
        {
        }
        protected override void Start()
        {
            _chosenIndex = -1;
            _markerController.InteractMarker.Hide();
        }
        public override void Update()
        {
            if (_inputService.IsChosenCell(out _chosenIndex) &&
                _chosenIndex < CurrentSize)
            {
                SelectCellByIndex(_chosenIndex);
                
            }

            ApplyItem();
        }
        void ApplyItem()
        {
            if (ChosenItem != null &&
                ChosenItem.ApplyCondition(_markerController.CurrentTarget))
            {
                _markerController.InteractMarker.Activate();

                if(_inputService.IsLBK())
                {
                    bool used = ChosenItem.Apply(_markerController.CurrentTarget);
                    if(used)
                    {
                        _player.UseItemVisual(ChosenItem);
                        
                        if(ChosenItem.Consumable)
                        {
                            ChosenItem.Count--;
                        }
                    }
                }
            }
            else
            {
                _markerController.InteractMarker.Hide();
            }
        }
        protected override void SaveInventory()
        {
            //_gameDataState.UpdateActivePackInventory(InventoryItems);
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
        void DeactivateOther(InventoryItem selectedItem)
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
