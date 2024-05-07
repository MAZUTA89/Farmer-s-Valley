using HappyHarvest;
using Scripts.PlacementCode;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "Basket", menuName = "SO/InventoryItems/Basket")]
    public class BasketItem : InventoryItem
    {
        public override bool Apply(Vector3Int target)
        {
            var product = PlacementService.Instance().HarvestAt(target);
            if(product != null)
            {
                if (PlayerInventory.Instance().TryAddItem(product.Produce))
                {
                    return true;
                }
                else { return false; }
            }
            return false;
        }

        public override bool ApplyCondition(Vector3Int target)
        {
            var data = PlacementService.Instance().GetCropDataAt(target);
            return data != null && data.GrowingCrop != null && Mathf.Approximately(data.GrowthRatio, 1.0f);
        }

        public override object Clone()
        {
            BasketItem basketItem =
                CreateInstance<BasketItem>();
            basketItem.InitializeCopy(this);
            return basketItem;
        }

        public override void RenderUI(InventoryCell inventoryCell)
        {
            base.RenderUI(inventoryCell);

        }
    }
}
