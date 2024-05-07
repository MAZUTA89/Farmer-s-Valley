using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "Product", menuName = "SO/InventoryItems/Product")]
    public class ProductItem : InventoryItem
    {
        public int SellPrice = 1;

        public override void InitializeCopy(InventoryItem inventoryItem)
        {
            base.InitializeCopy(inventoryItem);
            if(inventoryItem is ProductItem product)
            {
                SellPrice = product.SellPrice;
            }
        }
        public override bool Apply(Vector3Int target)
        {
            return true;
        }

        public override void RenderUI(InventoryCell inventoryCell)
        {
            base.RenderUI(inventoryCell);
        }

        public override bool ApplyCondition(Vector3Int target)
        {
            return Count > 0;
        }

        public override bool NeedTarget()
        {
            return false;
        }

        public override object Clone()
        {
            ProductItem productItem = 
                CreateInstance<ProductItem>();
            productItem.InitializeCopy(this);
            return productItem;
        }
    }
}
