using Assets.Scripts.Inventory.Items;
using Scripts.SO.InteractableObjects;
using Scripts.SO.InventoryItem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameSO.InventorySO
{
    [CreateAssetMenu(fileName = "BagQuantitativeToolItem",
        menuName = "SO/InventoryItems/BagQuantitativeToolItem")]
    public class BagQuantitativeToolItemAssetData : QuantitativeItemAssetData,
        IProductionInventoryItem<GameObject>
    {
        public GameObject ProductionObject { get; }
        []
        public SeedSO SeedSO { get; set; }
    }
}
