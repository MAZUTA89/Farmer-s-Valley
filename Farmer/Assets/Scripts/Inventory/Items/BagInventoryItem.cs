using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using Scripts.SaveLoader;
using Scripts.SO.InteractableObjects;
using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{ 
    [CreateAssetMenu(fileName = "BagInventoryItem",
            menuName = "SO/InventoryItems/BagInventoryItem")]
    public class BagInventoryItem : QuantitativeInventoryItem, IBagInventoryItem, IUsableInventoryItem
    {
        public SeedSO SeedSO => _seedSO;

        public Seed ProductionObject => _seedObject;
        public UseConditionSO UseConditionSO => _useConditionSO;

        [SerializeField] private UseConditionSO _useConditionSO;

        [SerializeField] private Seed _seedObject;

        [SerializeField] private SeedSO _seedSO;
        public override InventoryItemData GetItemData()
        {
           QuantitativeItemData quantitativeItemData =
                (QuantitativeItemData)base.GetItemData();
            BagInventoryItemData bagInventoryItemData =
                new BagInventoryItemData (quantitativeItemData);
            return bagInventoryItemData;
        }
    }
}
