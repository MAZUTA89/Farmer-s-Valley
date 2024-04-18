using Assets.Scripts.Inventory.Items;
using Scripts.SO.InventoryItem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameSO.InventorySO
{
    [CreateAssetMenu(fileName = "HoeItem",
        menuName = "SO/InventoryItems/HoeToolItem")]
    public class HoeToolItemAssetData : InventoryItemAssetData
        ,IProductionInventoryItem<RuleTile>
    {
        public RuleTile ProductionObject { get; }
    }
}
