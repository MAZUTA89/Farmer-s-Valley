using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.InteractableObjects
{
    public class ChestFactory : IInteractableObjectFactory<Chest, List<InventoryItem>>
    {
        Chest _chestTemplate;
        DiContainer _container;
        public ChestFactory()
        {
            
        }
        public ChestFactory(DiContainer diContainer, Chest chestTemplate) 
        {
            _chestTemplate = chestTemplate;
            _container = diContainer;
        }
        public Chest Create(List<InventoryItem> inventoryItems)
        {
            var chest
                = _container.InstantiatePrefabForComponent<Chest>(_chestTemplate);
            return chest;
        }
    }
}
