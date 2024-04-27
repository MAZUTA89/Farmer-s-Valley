using Assets.Scripts.ItemUsage;
using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using Scripts.PlacementCode;
using System;
using UnityEngine;
using Zenject;

namespace Scripts.ItemUsage
{
    public class ItemApplier
    {
        MapClicker _mapClicker;
        PlacementMapsContainer _placementMapsContainer;
        DiContainer _diContainer;
        IItemHandler _hoeItemHandler;  
        IItemHandler _bagItemHandler;
        IItemHandler _oakBagItemHandler;
        public ItemApplier(
            PlacementMapsContainer placementMapsContainer,
            MapClicker mapClicker,
            DiContainer diContainer)
        {
            _placementMapsContainer = placementMapsContainer;
            
            _mapClicker = mapClicker;
            _diContainer = diContainer;
            InitializeHandlers();
        }
        void InitializeHandlers()
        {
            _hoeItemHandler = new HoeItemHandler(_placementMapsContainer, _mapClicker);
            _bagItemHandler = new BagItemHandler(_mapClicker,
                _placementMapsContainer,
                _diContainer);

             _oakBagItemHandler = 
                new OakBagItemHandler(_mapClicker,
                _placementMapsContainer,
                _diContainer);

            _hoeItemHandler.Successor = _oakBagItemHandler;
            _oakBagItemHandler.Successor = _bagItemHandler;
            //_bagItemHandler.Successor = _hoeItemHandler;
        }
        public void ApplyItem(IInventoryItem inventoryItem)
        {
            _hoeItemHandler.HandleItem(inventoryItem);
        }
    }
}
