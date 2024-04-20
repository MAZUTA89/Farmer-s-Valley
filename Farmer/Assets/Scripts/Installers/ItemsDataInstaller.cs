using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.InventoryCode;
using Scripts.PlacementCode;
using UnityEngine.Tilemaps;
using Scripts.InteractableObjects;
using Scripts.SaveLoader;
using Assets.Scripts.Inventory.Items;

namespace Scripts.Installers
{
    public class ItemsDataInstaller : MonoInstaller
    {
        [Header("Все предметы инвентаря в игре")]
        [SerializeField] List<InventoryItem> _inventoryItemAssetList;

        [SerializeField] private ChestInventory InventoryStorageTemplate;

        [SerializeField] private Tilemap _gameElementsMap;
        [SerializeField] private Chest _chestTemplate;
        public override void InstallBindings()
        {
            BindInventoryItemsDictionary();
            //BindItemsData();
            BindPlacement();
            BindObjectsLogic();
        }
        void BindInventoryItemsDictionary()
        {
            Dictionary<string, IInventoryItem> InventoryItemsDictionary
                = new Dictionary<string, IInventoryItem>();

            foreach (var item in _inventoryItemAssetList)
            {
                InventoryItemsDictionary[item.Name] = item;
            }

            Container.BindInstance(InventoryItemsDictionary).AsSingle();
        }
        void BindObjectsLogic()
        {
            Container.Bind<ChestInventory>().FromInstance(InventoryStorageTemplate)
                .AsTransient();
            Container.Bind<IInventoryPanelFactory>()
                 .To<InventoryChestPanelFactory>()
                 //.WithArguments(Container, InventoryStorageTemplate)
                 .WhenInjectedInto<Chest>();

            Container.BindInstance(_chestTemplate).AsTransient();
            //Container.Bind<IInteractableObjectFactory<Chest, List<IInventoryItem>>>()
            //    .To<IChestFactory>().AsTransient();
            Container.Bind<IChestFactory>().To<ChestFactory>().AsTransient();

            Container.Bind<Chest>().FromComponentInHierarchy()
                .AsTransient();
        }
        void BindItemsData()
        {
            Container.Bind<IInventoryItem>()
                .To<InventoryItem>();
            Container.Bind<IQuantitativeInventoryItem>()
                .To<QuantitativeInventoryItem>();
            Container.Bind<IProductionInventoryItem<RuleTile>>()
                .To<IHoeInventoryItem>();
            Container.Bind<IHoeInventoryItem>()
                .To<HoeInventoryItem>();
            Container.Bind<IBagInventoryItem>()
                .To<BagInventoryItem>();
        }
        void BindPlacement()
        {
            ItemPlacementMap itemPlacementMap =
                new ItemPlacementMap(_gameElementsMap);
            Container.BindInstance(itemPlacementMap).AsTransient();
            Container.Bind<PlacementItem>().To<Chest>().AsTransient();
        }
    }
}
