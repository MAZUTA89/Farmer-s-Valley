using Scripts.InteractableObjects;
using Scripts.SO.InteractableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Inventory.Items
{
    public interface IBagInventoryItem : IProductionInventoryItem<Seed>
    {
        SeedSO SeedSO { get; }
    }
}
