using Assets.Scripts.ItemUsage;
using Scripts.InventoryCode;


namespace Scripts.ItemUsage
{
    public class ItemHandler : IItemHandler
    {
        public IItemHandler Successor { set; get; }


        public virtual void HandleItem(IInventoryItem inventoryItem)
        {
            Successor.HandleItem(inventoryItem);
        }
    }
}
