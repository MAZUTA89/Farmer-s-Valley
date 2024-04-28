using Assets.Scripts.ItemUsage;
using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using Scripts.PlacementCode;
using System;
using UnityEngine;
using Zenject;

namespace Scripts.ItemUsage
{
    public class ItemApplier
    {
        ItemApplierTools _itemApplierTools;
        IItemHandler _hoeItemHandler;  
        IItemHandler _bagItemHandler;
        IItemHandler _oakBagItemHandler;
        public ItemApplier(
            ItemApplierTools itemApplierTools)
        {
            _itemApplierTools = itemApplierTools;
            InitializeHandlers();
        }
        void InitializeHandlers()
        {
            _hoeItemHandler = new HoeItemHandler(_itemApplierTools);
            _bagItemHandler = new BagItemHandler(_itemApplierTools);

             _oakBagItemHandler = 
                new OakBagItemHandler(_itemApplierTools);

            _hoeItemHandler.Successor = _oakBagItemHandler;
            _oakBagItemHandler.Successor = _bagItemHandler;
            //_bagItemHandler.Successor = _hoeItemHandler;
        }
        public void ApplyItem(IInventoryItem inventoryItem)
        {
            _hoeItemHandler.HandleItem(inventoryItem);
        }
        public void PerformItemCondition(IUsableInventoryItem usableInventoryItem)
        {

        }
    }
}
