using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.ChestItem
{ 
    public class ChestFromResourceFactory : IChestFactory
    {
        const string c_resourcePath = @"/Prefabs/Chest/";
        const string c_prefabName = @"ChestBottom.prefab";
        DiContainer _container;
        public ChestFromResourceFactory(DiContainer diContainer)
        {
            _container = diContainer;
        }

        public Chest Create(List<InventoryItem> inventoryItems, Vector2Int vector2Int)
        {
            var chest =
                _container.InstantiatePrefabForComponent<Chest>
                (Resources.Load(Application.dataPath +
                c_resourcePath +
                c_prefabName));
            return chest;
        }
    }
}
