using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryCode.ItemResources
{
    public class ItemResourceDroper
    {
        Transform _playerTransform;
        IItemResourceFactory _factory;
        public ItemResourceDroper(
            [Inject(Id ="PlayerTransform")]Transform playerTransform,
            IItemResourceFactory itemResourceFactory)
        {
            _playerTransform = playerTransform;
            _factory = itemResourceFactory;
        }
        public void DropByPlayer(InventoryItem inventoryItem)
        {
            ItemResource itemResource = _factory.Create(inventoryItem, _playerTransform.position);
        }
    }
}
