using Scripts.InventoryCode;
using Scripts.PlacementCode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.ItemUsage
{
    public class HoeItemHandler : PressingItemHandler
    {
        SandTilePlacementMap _sandTilePlacementMap;
        public HoeItemHandler(ItemApplierTools itemApplierTools) 
            : base(itemApplierTools)
        {
            _sandTilePlacementMap = PlacementMapsContainer.SandTilePlacementMap;
        }
        
        protected override void HandleClick(IInventoryItem inventoryItem,
            Vector3Int clickedPosition)
        {
            IHoeInventoryItem hoe = inventoryItem as IHoeInventoryItem;
            if(UseCondition(inventoryItem, clickedPosition))
            {
                _sandTilePlacementMap.PlaceObjectOnCell(hoe.ProductionObject,
                    clickedPosition);
                _sandTilePlacementMap.AddPosition(clickedPosition);
            }
        }

        protected override bool UseCondition(IInventoryItem inventoryItem, Vector3Int position)
        {
            if (!_sandTilePlacementMap.IsOccupiedBySand(position) &&
                        !ItemPlacementMap.IsOccupied(position))
            {
                return true;
            }
            else
                return false;
        }
        protected override bool HandleCondition(IInventoryItem inventoryItem)
        {
            return inventoryItem is IHoeInventoryItem;
        }
    }
}
