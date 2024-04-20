using Scripts.FarmGameEvents;
using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using Zenject;

namespace Scripts.InventoryCode
{
    public class InventoryStorage : InventoryBase
    {
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
    }
}
