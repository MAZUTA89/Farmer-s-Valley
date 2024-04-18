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
        ActiveInventory _activeInventory;
        MapClicker _mapClicker;
        ItemPlacementMap _itemPlacementMap;
        IApplyItem _applyItem;
        Vector2Int _clickedPosition;
        public ItemApplier(ActiveInventory activeInventory,
            ItemPlacementMap itemPlacementMap)
        {
            _activeInventory = activeInventory;
            _itemPlacementMap = itemPlacementMap;
        }

        public void Update()
        {
            if (_activeInventory.ChosenItem.ItemType != ItemType.Resource)
            {
                if (_activeInventory.ChosenItem.ItemType == ItemType.Production)
                {
                    if (_mapClicker.IsClicked(out _clickedPosition))
                    {

                    }

                }
            }

        }
    }
}
