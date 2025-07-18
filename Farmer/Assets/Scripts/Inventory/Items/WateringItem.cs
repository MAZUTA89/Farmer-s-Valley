﻿using Scripts.PlacementCode;
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

        public override object Clone()
        {
            WateringItem wateringItem =
                CreateInstance<WateringItem>();
            wateringItem.InitializeCopy(this);
            return wateringItem;
        }
        public override InventoryItemData GetData()
        {
            WateringItemData wateringItemData = new();
            wateringItemData.Init(base.GetData());
            return wateringItemData;
        }
    }
}
