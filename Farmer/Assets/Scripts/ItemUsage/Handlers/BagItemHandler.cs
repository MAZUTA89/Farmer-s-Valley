using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using Scripts.PlacementCode;
using Scripts.SO.InteractableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Scripts.ItemUsage
{
    public class BagItemHandler : ItemHandler
    {
        protected MapClicker MapClicker;
        protected SandTilePlacementMap SandTilePlacementMap;
        protected ItemPlacementMap SeedPlacementMap;
        protected ISeedFactory SeedFactory;
        DiContainer _diContainer;
        public BagItemHandler(MapClicker mapClicker,
            PlacementMapsContainer placementsContainer,
            DiContainer diContainer)
        {
            MapClicker = mapClicker;
            SandTilePlacementMap = placementsContainer.SandTilePlacementMap;
            SeedPlacementMap = placementsContainer.SeedPlacementMap;
            _diContainer = diContainer;
        }
        public override void HandleItem(IInventoryItem inventoryItem)
        {
            if (HandleCondition(inventoryItem))
            {
                IBagInventoryItem bagItem = inventoryItem as IBagInventoryItem;
                if (bagItem.Count > 0)
                {
                    if (MapClicker.IsClicked(out Vector3Int clickedPosition))
                    {
                        if (SeedFactory == null)
                        {
                            SeedFactory = new SeedFactory(_diContainer,
                                bagItem.ProductionObject);
                        }
                        if(UseBag(clickedPosition, bagItem.SeedSO))
                        {
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
        protected virtual bool HandleCondition(IInventoryItem inventoryItem)
        {
            Debug.Log("Bag item condition!");
            return inventoryItem is IBagInventoryItem;
        }
        protected virtual bool UseBag(Vector3Int clickedPosition,
            SeedSO seedSO)
        {
            if (SandTilePlacementMap.IsOccupiedBySand(clickedPosition) &&
                       !SeedPlacementMap.IsOccupied(clickedPosition))
            {
                Seed seed = SeedFactory.Create(seedSO);
                SandTilePlacementMap.PlaceObjectOnCell(seed.gameObject,
                    clickedPosition);
                return true;
            }
            else { return false; }
        }
    }
}
