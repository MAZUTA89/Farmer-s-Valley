using Assets.Scripts.Installers;
using Scripts.InventoryCode;
using Scripts.PlacementCode;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Scripts.ItemUsage
{
    public class ItemApplier
    {
        MapClicker _mapClicker;
        ItemPlacementMap _itemPlacementMap;
        IApplyItem _applyItem;
        Vector2Int _clickedPosition;
        public ItemApplier(ItemPlacementMap itemPlacementMap)
        {
            _activeInventory = activeInventory;
            _itemPlacementMap = itemPlacementMap;
        }
        public void ApplyItem(IInventoryItem inventoryItem)
        {

        }
    }
}
