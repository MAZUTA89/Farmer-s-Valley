using HappyHarvest;
using Scripts.PlacementCode;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "Watering", menuName = "SO/InventoryItems/Watering")]
    public class WateringItem : InventoryItem
    {
        public override bool Apply(Vector3Int target)
        {
             PlacementService.Instance().WaterAt(target);
            return true;
        }

        public override bool ApplyCondition(Vector3Int target)
        {
            return PlacementService.Instance().IsTilled(target);
        }
    }
}
