using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.ChestItem
{
    public class ChestFactory : IChestFactory
    {
        Chest _chestTemplate;
        DiContainer _container;
        public ChestFactory(DiContainer diContainer, Chest chestTemplate) 
        {
            _chestTemplate = chestTemplate;
            _container = diContainer;
        }
        public Chest Create(List<InventoryItem> inventoryItems, Vector2Int vector2Int)
        {
            var chest
                = _container.InstantiatePrefabForComponent<Chest>(_chestTemplate);
            return chest;
        }
    }
}
