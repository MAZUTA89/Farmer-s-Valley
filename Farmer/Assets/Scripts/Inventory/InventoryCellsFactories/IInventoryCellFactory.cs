using UnityEngine;


namespace Scripts.InventoryCode
{
    public interface IInventoryCellFactory
    {
        InventoryCell Create(Transform visualContext);
        GameObject CreateEmpty(InventoryCell inventoryCell);
    }
}
