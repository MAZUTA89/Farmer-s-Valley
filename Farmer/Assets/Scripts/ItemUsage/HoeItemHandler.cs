using Scripts.InventoryCode;
using Scripts.PlacementCode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.ItemUsage
{
    public class HoeItemHandler : ItemHandler
    {
        SandTilePlacementMap _sandTilePlacementMap;
        ItemPlacementMap _itemPlacementMap;
        MapClicker _itemMapClicker;
        public HoeItemHandler(SandTilePlacementMap sandTilePlacementMap,
            ItemPlacementMap itemPlacementMap,
            MapClicker itemMapClicker)
        {
            _sandTilePlacementMap = sandTilePlacementMap;
            _itemPlacementMap = itemPlacementMap;
            _itemMapClicker = itemMapClicker;
        }
        public override void HandleItem(IInventoryItem inventoryItem)
        {
            if(inventoryItem is IHoeInventoryItem hoe)
            {
                Vector3Int clickedPosition;
                if(_itemMapClicker.IsClicked(out clickedPosition))
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
    }
}
