using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using Scripts.PlacementCode;
using Scripts.PlayerCode;
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
        public BagItemHandler(ItemApplierTools itemApplierTools, Player player, ISeedFactory seedFactory) :
            base(itemApplierTools, player)

        {
            SeedFactory = seedFactory;
            SandTilePlacementMap = PlacementMapsContainer.SandTilePlacementMap;
            SeedPlacementMap = PlacementMapsContainer.SeedPlacementMap;
            _diContainer = DiContainer;
        }
        protected override void HandleClick(IInventoryItem inventoryItem,
            Vector3Int clickedPosition)
        {
            IBagInventoryItem bagItem = inventoryItem as IBagInventoryItem;

            if (SeedFactory == null)
            {
                SeedFactory = new SeedFactory(_diContainer,
                    bagItem.ProductionObject);
            }
            if (UseCondition(inventoryItem, clickedPosition))
            {
                UseBag(clickedPosition, bagItem.SeedSO);
                bagItem.Count--;
            }

        }
        protected override bool HandleCondition(IInventoryItem inventoryItem)
        {
            Debug.Log("Bag item condition!");
            return inventoryItem is IBagInventoryItem;
        }
        protected virtual void UseBag(Vector3Int clickedPosition,
            SeedSO seedSO)
        {
            Seed seed = SeedFactory.Create(seedSO);
            SandTilePlacementMap.PlaceObjectOnCell(seed.gameObject,
                clickedPosition);
        }
        protected override bool UseCondition(IInventoryItem inventoryItem, Vector3Int position)
        {
            IBagInventoryItem bagInventoryItem = inventoryItem as IBagInventoryItem;
            if (bagInventoryItem.Count > 0)
            {
                if (SandTilePlacementMap.IsOccupiedBySand(position) &&
                           !SeedPlacementMap.IsOccupied(position))
                {
                    return true;
                }
                else { return false; }
            }
            else
            {
                return false;
            }
        }
    }
}
