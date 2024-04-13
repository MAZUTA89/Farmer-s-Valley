using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

namespace Scripts.ChestItem
{
    public class InventoryChestPanelFactory : IInventoryPanelFactory
    {
        DiContainer _container;
        ChestInventory _storageTemplate;
        [Inject(Id = "DragParent")] Transform _globalVisualContext;
        public InventoryChestPanelFactory(DiContainer diContainer,
            [Inject(Id = "ChestPanelTemplate")] ChestInventory storageTemplate
           )
        {
            _container = diContainer;
            _storageTemplate = storageTemplate;
            // _globalVisualContext = globalVisualContext;
        }
        public InventoryBase Create(List<InventoryItem> inventoryItems)
        {
            var storage = _container.
            InstantiatePrefabForComponent<ChestInventory>(_storageTemplate,
            _globalVisualContext);
            storage.Initialize(inventoryItems);
            return storage;
        }
    }
}
