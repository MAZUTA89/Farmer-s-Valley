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
        ChestInventory _storageTemplate;
        [Inject(Id = "DragParent")] Transform _globalVisualContext;
        public InventoryChestPanelFactory(DiContainer diContainer,
            /*[Inject(Id = "ChestPanelTemplate")]*/ ChestInventory storageTemplate
           )
        {
            _container = diContainer;
            _storageTemplate = storageTemplate;
        }
        public InventoryBase Create(List<IInventoryItem> inventoryItems)
        {
            var storage = _container.
            InstantiatePrefabForComponent<ChestInventory>(_storageTemplate,
            _globalVisualContext);
            storage.Initialize(inventoryItems);
            return storage;
        }
    }
}
