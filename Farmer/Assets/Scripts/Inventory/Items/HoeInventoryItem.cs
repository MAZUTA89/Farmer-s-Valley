﻿using Scripts.InventoryCode;
using UnityEngine;

namespace Assets.Scripts.Inventory.Items
{
    [CreateAssetMenu(fileName = "HoeInventoryItem",
            menuName = "SO/InventoryItems/HoeInventoryItem")]
    public class HoeInventoryItem : InventoryItem, IHoeInventoryItem
    {
        public RuleTile ProductionObject => _sandTile;
        [SerializeField] private RuleTile _sandTile;
    }
}