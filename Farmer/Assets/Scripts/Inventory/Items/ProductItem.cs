using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "Product", menuName = "SO/InventoryItems/Product")]
    public class ProductItem : InventoryItem
    {
        public int SellPrice = 1;
        public override bool Apply(Vector3Int target)
        {
            throw new NotImplementedException();
        }

        public override bool ApplyCondition(Vector3Int target)
        {
            throw new NotImplementedException();
        }
    }
}
