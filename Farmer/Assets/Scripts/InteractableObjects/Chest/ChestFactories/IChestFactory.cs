using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.InteractableObjects
{
    public interface IChestFactory : IInteractableObjectFactory< Chest, List<IInventoryItem>>
    {
    }
}
