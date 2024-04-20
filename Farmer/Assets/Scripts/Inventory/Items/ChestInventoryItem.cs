using Scripts.InteractableObjects;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    [CreateAssetMenu(fileName = "ChestInventoryItem",
            menuName = "SO/InventoryItems/ChestInventoryItem")]
    public class ChestInventoryItem : QuantitativeInventoryItem, IChestInventoryItem
    {
        public List<IInventoryItem> Items {  get; set; }
        public Chest ProductionObject => _chest;

        [SerializeField] private Chest _chest;


        public override InventoryItemData GetItemData()
        {
            List<InventoryItemData> inventoryItemDatas = new List<InventoryItemData>();
            foreach (var item in Items)
            {
                inventoryItemDatas.Add(item.GetItemData());
            }
            QuantitativeItemData quantitativeInventoryItem
                = (QuantitativeItemData)base.GetItemData();
            ChestInventoryItemData data =
                new ChestInventoryItemData(quantitativeInventoryItem,
                inventoryItemDatas);
            return data;
        }

    }
}
