using Scripts.SaveLoader;
using System;
using UnityEngine;

namespace Scripts.InventoryCode
{
    public interface IInventoryItem
    {
        string Name { get; }
        Sprite Icon { get; }
        Color Color { get; }
        bool IsSelected { get; set; }
        void RenderUI(InventoryCell inventoryCell);
        InventoryItemData GetItemData();
    }
}
