using Scripts.SO.Inventory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "InventoryItem",
            menuName = "SO/InventoryItems/UsableInventoryItem")]
    public class UsableInventoryItem : InventoryItem, IUsableInventoryItem
    {
        public UseConditionSO UseConditionSO => _useConditionSO;


        [SerializeField] private UseConditionSO _useConditionSO;
    }
}
