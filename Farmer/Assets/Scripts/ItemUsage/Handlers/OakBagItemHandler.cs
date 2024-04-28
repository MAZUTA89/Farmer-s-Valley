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

        protected override bool UseBag(Vector3Int clickedPosition, SeedSO seedSO)
        {
            ///смотрим, есть ли клик по песку или по другому предмету,
            ///если нет песка или предмета то идем дальше
            if (!PlacementMap.IsOccupied(clickedPosition) &&
                !SandTilePlacementMap.IsOccupied(clickedPosition))
            {
                OakSeedSO treeSeedSO = seedSO as OakSeedSO;
                List<Vector3Int> positions = treeSeedSO.GetCellsPosition(clickedPosition);
                // если точки дерева не пересекаются с другими точками и с песком, то ставим
                if (PlacementMap.IsOccupied(positions) == false 
                    /*SandTilePlacementMap.IsOccupiedBySand(positions) == false*/)
                {
                    var seed = SeedFactory.Create(seedSO);
                    PlacementMap.PlaceObjectOnCell(seed.gameObject, clickedPosition);
                    return true;
                }
                else return false;
            }
            else return false;
        }
        protected override bool HandleCondition(IInventoryItem inventoryItem)
        {
            Debug.Log("OakBag item condition!");
            return inventoryItem is IOakBagInventoryItem;
        }
        protected override bool UseCondition(IInventoryItem inventoryItem, Vector3 position)
        {
            Vector3Int pos = SandTilePlacementMap.Vector3ConvertToVector3Int(position);
            ///смотрим, есть ли клик по песку или по другому предмету,
            ///если нет песка или предмета то идем дальше
            if (!PlacementMap.IsOccupied(pos) &&
                !SandTilePlacementMap.IsOccupied(pos))
            {
                IOakBagInventoryItem bagItem = inventoryItem as IOakBagInventoryItem;
                SeedSO seedSO = bagItem.SeedSO;
                OakSeedSO treeSeedSO = seedSO as OakSeedSO;
                List<Vector3Int> positions = treeSeedSO.GetCellsPosition(pos);
                // если точки дерева не пересекаются с другими точками и с песком, то ставим
                if (PlacementMap.IsOccupied(positions) == false 
                    /*SandTilePlacementMap.IsOccupiedBySand(positions) == false*/)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}
