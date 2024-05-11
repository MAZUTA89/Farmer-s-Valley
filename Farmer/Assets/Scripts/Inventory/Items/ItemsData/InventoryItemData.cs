using System;
using System.Collections.Generic;


namespace Scripts.InventoryCode
{
    public class InventoryItemData
    {
        public string SoName;
        public int Amount;
        public string InventoryItemTypeName;
        public InventoryItemData() 
        {
            InventoryItemTypeName = nameof(InventoryItemData);
        }
        public void Init(InventoryItemData inventoryItemData)
        {
            SoName = inventoryItemData.SoName;
            Amount = inventoryItemData.Amount;
        }
    }
    public class BasketItemData : InventoryItemData 
    {
        public BasketItemData()
        {
             InventoryItemTypeName = nameof (BasketItemData);
        }
    }
    public class HoeItemData : InventoryItemData
    {
        public HoeItemData()
        {
            InventoryItemTypeName = nameof(HoeItemData);
        }
    }
    public class ProductItemData : InventoryItemData
    {
        public ProductItemData()
        {
            InventoryItemTypeName = nameof(ProductItemData);
        }
    }
    public class SeedBagItemData : InventoryItemData
    {
        public SeedBagItemData()
        {
            InventoryItemTypeName = nameof(SeedBagItemData);
        }
    }
    public class WateringItemData : InventoryItemData
    {
        public WateringItemData()
        {
            InventoryItemTypeName = nameof(WateringItemData);
        }
    }
}
