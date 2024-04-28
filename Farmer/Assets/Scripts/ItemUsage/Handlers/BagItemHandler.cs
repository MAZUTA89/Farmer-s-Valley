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
    public class BagItemHandler : PressingItemHandler
    {
        protected SandTilePlacementMap SandTilePlacementMap;
        protected ItemPlacementMap SeedPlacementMap;
        protected ISeedFactory SeedFactory;
        DiContainer _diContainer;
        public BagItemHandler(ItemApplierTools itemApplierTools) :
            base(itemApplierTools)

        {
            SandTilePlacementMap = PlacementMapsContainer.SandTilePlacementMap;
            SeedPlacementMap = PlacementMapsContainer.SeedPlacementMap;
            _diContainer = DiContainer;
        }
        public override void HandleItem(IInventoryItem inventoryItem)
        {
            base.HandleItem(inventoryItem);

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
        protected override bool UseCondition(IInventoryItem inventoryItem, Vector3 position)
        {
            Vector3Int pos = SandTilePlacementMap.Vector3ConvertToVector3Int(position);

            if (SandTilePlacementMap.IsOccupiedBySand(pos) &&
                       !SeedPlacementMap.IsOccupied(pos))
            {
                return true;
            }
            else { return false; }
        }
    }
}
