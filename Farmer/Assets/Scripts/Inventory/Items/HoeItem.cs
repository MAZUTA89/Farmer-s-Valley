using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "Hoe", menuName = "SO/InventoryItems/Hoe")]
    public class HoeItem : InventoryItem
    {
        public override bool Apply(Vector3Int target)
        {
            return false;
        }

        public override bool ApplyCondition(Vector3Int target)
        {
            return true;
        }
    }
}
