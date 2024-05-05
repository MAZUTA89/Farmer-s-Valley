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
            throw new NotImplementedException();
        }

        public override bool ApplyCondition(Vector3Int target)
        {
            throw new NotImplementedException();
        }
        public override void RenderUI(InventoryCell inventoryCell)
        {
            base.RenderUI(inventoryCell);

        }
    }
}
