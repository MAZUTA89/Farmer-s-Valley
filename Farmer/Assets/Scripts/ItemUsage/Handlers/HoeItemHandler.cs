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
        ItemPlacementMap _itemPlacementMap;
        public HoeItemHandler(ItemApplierTools itemApplierTools) :
            base(itemApplierTools)
        {
            _sandTilePlacementMap = PlacementMapsContainer.SandTilePlacementMap;
            _itemPlacementMap = PlacementMapsContainer.ItemPlacementMap;
        }
        public override void HandleItem(IInventoryItem inventoryItem)
        {
            base.HandleItem(inventoryItem);
            if(inventoryItem is IHoeInventoryItem hoe)
            {
                Vector3Int clickedPosition;
                if(MapClicker.IsClicked(out clickedPosition))
                {
                    if(!_sandTilePlacementMap.IsOccupiedBySand(clickedPosition) &&
                        !_itemPlacementMap.IsOccupied(clickedPosition))
                    {
                        _sandTilePlacementMap.PlaceObjectOnCell(hoe.ProductionObject, 
                            clickedPosition);
                        _sandTilePlacementMap.AddPosition(clickedPosition);
                    }
                }
            }
            else
            {
                Successor.HandleItem(inventoryItem);
            }
        }

        protected override bool UseCondition(IInventoryItem inventoryItem, Vector3 position)
        {
            Vector3Int pos = _sandTilePlacementMap.Vector3ConvertToVector3Int(position);

            if (!_sandTilePlacementMap.IsOccupiedBySand(pos) &&
                        !_itemPlacementMap.IsOccupied(pos))
            {
                return true;
            }
            else
                return false;
        }
    }
}
