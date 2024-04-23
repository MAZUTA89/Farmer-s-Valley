using Assets.Scripts.ItemUsage;
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
        ItemPlacementMap _itemPlacementMap;
        SandTilePlacementMap _sandTilePlacementMap;
        DiContainer _diContainer;
        Vector2Int _clickedPosition;
        IItemHandler _hoeItemHandler;
        IItemHandler _bagItemHandler;
        public ItemApplier(ItemPlacementMap itemPlacementMap,
            SandTilePlacementMap sandTilePlacementMap,
            MapClicker mapClicker,
            DiContainer diContainer)
        {
            _itemPlacementMap = itemPlacementMap;
            _sandTilePlacementMap = sandTilePlacementMap;
            _mapClicker = mapClicker;
            _diContainer = diContainer;
            InitializeHandlers();
        }
        void InitializeHandlers()
        {
            _hoeItemHandler = new HoeItemHandler(_sandTilePlacementMap,
                _itemPlacementMap, _mapClicker);
            _bagItemHandler = new BagItemHandler(_mapClicker,
                _sandTilePlacementMap,
                _diContainer);
            _hoeItemHandler.Successor = _bagItemHandler;
        }
        public void ApplyItem(IInventoryItem inventoryItem)
        {
            _hoeItemHandler.HandleItem(inventoryItem);
        }
    }
}
