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
        Vector2Int _clickedPosition;
        IItemHandler _hoeItemHandler;
        IItemHandler _bagItemHandler;
        public ItemApplier(ItemPlacementMap itemPlacementMap,
            SandTilePlacementMap sandTilePlacementMap)
        {
            _itemPlacementMap = itemPlacementMap;
            _sandTilePlacementMap = sandTilePlacementMap;


        }
        public void ApplyItem(IInventoryItem inventoryItem)
        {

        }
    }
}
