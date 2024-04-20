using Scripts.InteractableObjects;
using Scripts.SO.InteractableObjects;

namespace Scripts.InventoryCode
{
    public interface IBagInventoryItem : IProductionInventoryItem<Seed>, IQuantitativeInventoryItem
    {
        SeedSO SeedSO { get; }
    }
}
