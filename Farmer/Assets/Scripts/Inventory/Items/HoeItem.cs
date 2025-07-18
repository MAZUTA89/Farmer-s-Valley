﻿using Scripts.PlacementCode;
using UnityEngine;

namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "Hoe", menuName = "SO/InventoryItems/Hoe")]
    public class HoeItem : InventoryItem
    {
        public override bool Apply(Vector3Int target)
        {
            PlacementService.Instance().TillAt(target);
            return true;
        }

        public override bool ApplyCondition(Vector3Int target)
        {

            return PlacementService.Instance() != null &&
                PlacementService.Instance().IsTillable(target);
        }

        public override object Clone()
        {
            HoeItem hoeItem =
                CreateInstance<HoeItem>();
            hoeItem.InitializeCopy(this);
            return hoeItem;
        }
        public override InventoryItemData GetData()
        {
            HoeItemData hoeItemData = new();
            hoeItemData.Init(base.GetData());
            return hoeItemData;
        }
    }
}
