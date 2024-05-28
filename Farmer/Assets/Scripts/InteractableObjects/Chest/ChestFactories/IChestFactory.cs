using Scripts.InventoryCode;
using System.Collections.Generic;

namespace Scripts.InteractableObjects
{
    public interface IChestFactory : IGameObjectFactory< Chest, List<InventoryItem>>
    {
    }
}
