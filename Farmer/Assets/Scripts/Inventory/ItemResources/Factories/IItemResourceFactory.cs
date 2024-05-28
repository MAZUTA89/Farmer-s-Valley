using UnityEngine;
namespace Scripts.InventoryCode.ItemResources
{
    public interface IItemResourceFactory
    {
        ItemResource Create(InventoryItem inventoryItem, Vector3 position);
    }
}


