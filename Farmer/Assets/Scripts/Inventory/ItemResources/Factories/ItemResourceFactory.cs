using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryCode.ItemResources
{
    public class ItemResourceFactory : IItemResourceFactory
    {
        ItemResource _itemResourceTemplate;
        DiContainer _container;
        public ItemResourceFactory(DiContainer diContainer, ItemResource itemResourceTemplate)
        {
            _itemResourceTemplate = itemResourceTemplate;
            _container = diContainer;
        }
        public ItemResource Create(InventoryItem inventoryItem,
            Vector3 position)
        {
            var item = _container.InstantiatePrefabForComponent<ItemResource>
                (_itemResourceTemplate,
                position, Quaternion.identity, null);
            item.Initialize(inventoryItem);
            return item;
        }
    }
}
