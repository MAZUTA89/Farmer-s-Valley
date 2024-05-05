using HappyHarvest;
using Scripts.PlacementCode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "SeedBag", menuName = "SO/InventoryItems/SeedBag")]
    public class SeedBagItem : InventoryItem
    {
        public Crop CropToPlant;
       
        public override bool Apply(Vector3Int target)
        {
            PlacementService.Instance().PlantAt(target, CropToPlant);
            return true;
        }

        public override bool ApplyCondition(Vector3Int target)
        {
            return PlacementService.Instance().IsPlantable(target);
        }
        public override void RenderUI(InventoryCell inventoryCell)
        {
            base.RenderUI(inventoryCell);
        }
    }
}
