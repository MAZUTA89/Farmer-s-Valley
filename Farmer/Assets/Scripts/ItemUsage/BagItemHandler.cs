using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using Scripts.PlacementCode;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Scripts.ItemUsage
{
    public class BagItemHandler : ItemHandler
    {
        MapClicker _mapClicker;
        SandTilePlacementMap _sandTilePlacementMap;
        ItemPlacementMap _seedPlacementMap;
        ISeedFactory _seedFactory;
        DiContainer _diContainer;
        public BagItemHandler(MapClicker mapClicker,
            SandTilePlacementMap sandTilePlacementMap,
            ItemPlacementMap seedPlacementMap,
            DiContainer diContainer)
        {
            _mapClicker = mapClicker;
            _sandTilePlacementMap = sandTilePlacementMap;
            _seedPlacementMap = seedPlacementMap;
            _diContainer = diContainer;
        }
        public override void HandleItem(IInventoryItem inventoryItem)
        {
            if(inventoryItem is IBagInventoryItem bagItem)
            {
                Vector3Int clickedPosition;
                if(_mapClicker.IsClicked(out clickedPosition))
                {
                    if(_sandTilePlacementMap.IsOccupiedBySand(clickedPosition) &&
                        !_seedPlacementMap.IsOccupied(clickedPosition))
                    {
                        if(_seedFactory == null)
                        {
                            _seedFactory = new SeedFactory(_diContainer,
                                bagItem.ProductionObject);
                        }
                        if(bagItem.Count > 0)
                        {
                            Seed seed = _seedFactory.Create(bagItem.SeedSO);
                            _sandTilePlacementMap.PlaceObjectOnCell(seed.gameObject,
                                clickedPosition);
                            bagItem.Count--;
                        }
                    }
                }
            }
            else
            {
                Successor?.HandleItem(inventoryItem);
            }
        }
    }
}
