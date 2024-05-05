using Assets.Scripts.ItemUsage;
using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using Scripts.PlacementCode;
using System;
using UnityEngine;
using Zenject;
using Scripts.PlayerCode;

namespace Scripts.ItemUsage
{
    public class ItemApplier
    {
        Player _player;
        ItemApplierTools _itemApplierTools;
        IItemHandler _hoeItemHandler;  
        IItemHandler _bagItemHandler;
        IItemHandler _oakBagItemHandler;
        InteractableObjectsFactoryProvider _factoryProvider;
        public ItemApplier(
            ItemApplierTools itemApplierTools, Player player,
            InteractableObjectsFactoryProvider interactableObjectsFactoryProvider)
        {
            _factoryProvider = interactableObjectsFactoryProvider;
            _itemApplierTools = itemApplierTools;
            _player = player;
            InitializeHandlers();
        }
        void InitializeHandlers()
        {
            //_hoeItemHandler = new HoeItemHandler(_itemApplierTools, _player,
            //    (SandFactory)_factoryProvider.GetFactory<SandFactory>());

            //_bagItemHandler = new BagItemHandler(_itemApplierTools, _player,
            //    (SeedFactory)_factoryProvider.GetFactory<SeedFactory>());

            // _oakBagItemHandler = 
            //    new OakBagItemHandler(_itemApplierTools, _player,
            //    (OakSeedFactory)_factoryProvider.GetFactory<OakSeedFactory>());

            //_hoeItemHandler.Successor = _oakBagItemHandler;
            //_oakBagItemHandler.Successor = _bagItemHandler;
            //_bagItemHandler.Successor = _hoeItemHandler;
        }
        public void ApplyItem(InventoryItem inventoryItem)
        {
            _hoeItemHandler.HandleItem(inventoryItem);
        }
        public void PerformItemCondition(IUsableInventoryItem usableInventoryItem)
        {

        }
    }
}
