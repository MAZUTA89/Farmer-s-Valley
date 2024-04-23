using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace Scripts.InventoryCode
{
    public class InventoryChestPanelFactory : IInventoryPanelFactory
    {
        DiContainer _container;
        ChestInventory _chestInventoryTemplate;
        [Inject(Id = "DragParent")] Transform _globalVisualContext;
        public InventoryChestPanelFactory(DiContainer diContainer,
            /*[Inject(Id = "ChestPanelTemplate")]*/ ChestInventory chestInventoryTemplate
           )
        {
            _container = diContainer;
            _chestInventoryTemplate = chestInventoryTemplate;
        }
        public InventoryBase Create(List<IInventoryItem> inventoryItems)
        {
            var storage = _container.
            InstantiatePrefabForComponent<ChestInventory>(_chestInventoryTemplate,
            _globalVisualContext);
            storage.Initialize(inventoryItems);
            return storage;
        }
    }
}
