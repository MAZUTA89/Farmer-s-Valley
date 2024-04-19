using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Assets.Scripts.Installers;

namespace Scripts.InventoryCode
{
    public class ActiveInventory : InventoryStorage
    {
        InputService _inputService;
        int _chosenIndex;
        public IInventoryItem ChosenItem { get; private set; }

        [Inject]
        public void ConstructActive(InputService inputService)
        {
            _inputService = inputService;
        }

        public override void ConstructStorage([Inject(Id = "ActiveInventoryInfo")] InventoryInfo inventoryInfo,
            IInventoryCellFactory inventoryCellFactory)
        {
            base.ConstructStorage(inventoryInfo, inventoryCellFactory);
        }
        protected override void Start()
        {
            _chosenIndex = -1;
        }
        private void Update()
        {
            if(_inputService.IsChosenCell(out _chosenIndex))
            {
                ChosenItem = InventoryItems[_chosenIndex];
            }
        }
    }
}
