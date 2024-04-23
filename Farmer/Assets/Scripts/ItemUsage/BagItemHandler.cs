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
        SandTilePlacementMap _tilePlacementMap;
        ISeedFactory _seedFactory;
        DiContainer _diContainer;
        public BagItemHandler(MapClicker mapClicker,
            SandTilePlacementMap sandTilePlacementMap,
            DiContainer diContainer)
        {
            _mapClicker = mapClicker;
            _tilePlacementMap = sandTilePlacementMap;
            _diContainer = diContainer;
        }
        public override void HandleItem(IInventoryItem inventoryItem)
        {
            if(inventoryItem is IBagInventoryItem bagItem)
            {
                Vector3Int clickedPosition;
                if(_mapClicker.IsClicked(out clickedPosition))
                {
                    if(_tilePlacementMap.IsOccupiedBySand(clickedPosition))
                    {
                        if(_seedFactory == null)
                        {
                            _seedFactory = new SeedFactory(_diContainer,
                                bagItem.ProductionObject);
                        }
                        Seed seed = _seedFactory.Create(bagItem.SeedSO);
                        _tilePlacementMap.PlaceObjectOnCell(seed.gameObject,
                            clickedPosition);
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
