using Scripts.InteractableObjects;
using Scripts.SO.InteractableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripts.InventoryCode
{
    public interface IBagInventoryItem : IProductionInventoryItem<Seed>, IQuantitativeInventoryItem
    {
        SeedSO SeedSO { get; }
    }
}
