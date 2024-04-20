using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryCode
{
    public class InventoryStoragePanelFactory : IInventoryPanelFactory
    {
        DiContainer _container;
        InventoryStorage _storageTemplate;
        [Inject(Id = "DragParent")] Transform _globalVisualContext;
        public InventoryStoragePanelFactory(DiContainer diContainer,
            [Inject(Id = "StoragePanelTemplate")]InventoryStorage storageTemplate
           )
        {
            _container = diContainer;
            _storageTemplate = storageTemplate;
        }
        public InventoryBase Create(List<IInventoryItem> inventoryItems)
        {
            var storage = _container.
            InstantiatePrefabForComponent<InventoryStorage>(_storageTemplate,
            _globalVisualContext);
            storage.Initialize(inventoryItems);
            return storage;
        }
    }
}
