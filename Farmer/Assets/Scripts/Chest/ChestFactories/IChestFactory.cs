using Scripts.ChestItem;
using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.ChestItem
{
    public interface IChestFactory
    {
        Chest Create(List<InventoryItem> inventoryItems, Vector2Int vector2Int);
    }
}
