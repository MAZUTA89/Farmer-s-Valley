using Scripts.InventoryCode;
using Scripts.ItemUsage;
using Scripts.PlacementCode;
using Scripts.SO.InteractableObjects;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.InteractableObjects
{
    public class OakBagItemHandler : BagItemHandler
    {
        protected ItemPlacementMap PlacementMap;
        public OakBagItemHandler(ItemApplierTools itemApplierTools)
            : base(itemApplierTools)
        {
            PlacementMap = itemApplierTools.PlacementMapsContainer.ItemPlacementMap;
        }

        protected override void UseBag(Vector3Int clickedPosition, SeedSO seedSO)
        {
            OakSeedSO treeSeedSO = seedSO as OakSeedSO;
            var seed = SeedFactory.Create(treeSeedSO);
            PlacementMap.PlaceObjectOnCell(seed.gameObject, clickedPosition);
        }
        protected override bool HandleCondition(IInventoryItem inventoryItem)
        {
            Debug.Log("OakBag item condition!");
            return inventoryItem is IOakBagInventoryItem;
        }
        protected override bool UseCondition(IInventoryItem inventoryItem, Vector3Int position)
        {
            IOakBagInventoryItem bagItem = inventoryItem as IOakBagInventoryItem;
            if(bagItem.Count > 0)
            {
                ///смотрим, есть ли клик по песку или по другому предмету,
                ///если нет песка или предмета то идем дальше
                if (!PlacementMap.IsOccupied(position) &&
                    !SandTilePlacementMap.IsOccupied(position))
                {

                    SeedSO seedSO = bagItem.SeedSO;
                    OakSeedSO treeSeedSO = seedSO as OakSeedSO;
                    List<Vector3Int> positions = treeSeedSO.GetCellsPosition(position);
                    // если точки дерева не пересекаются с другими точками и с песком, то ставим
                    if (PlacementMap.IsOccupied(positions) == false &&
                        SandTilePlacementMap.IsOccupiedBySand(positions) == false)
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else
            {
                return false;
            }
            
        }
    }
}
