﻿using Scripts.InventoryCode;
using Scripts.InventoryCode.ItemResources;
using Scripts.SO.Inventory;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        [Header("Canvas")]
        [SerializeField] Transform DragParent;
        [Header("Cell prefab")]
        [SerializeField] private InventoryCell CellTemplate;
        [Header("Empty cell prefab")]
        [SerializeField] private GameObject EmptyCellTemplate;
        [Space]
        [Header("Active inventory")]
        [SerializeField] private InventoryInfo _activeInventoryInfo;
        [Space]
        [Space]
        [Header("Backpack inventory")]
        [SerializeField] private InventoryInfo _storageInventoryInfo;
        [Space]
        [Header("ItemResourceSO")]
        [SerializeField] private ItemSourceSO ItemSourceSO;
        [Header("Item resource prefab")]
        [SerializeField] private ItemResource ItemResourcePrefab;
        [Header("Player Inventory object:")]
        [SerializeField] private PlayerInventory _playerInventory;
       
        public override void InstallBindings()
        {
            Container.BindInstance(_playerInventory).AsSingle();
            BindInventories();
            BindCellTemplate();
            BindGlobalVisualContext();
            BindFactories();
            BindItemResourceLogic();
            BindCells();
        }
        void BindGlobalVisualContext()
        {
            Container.BindInstance(DragParent)
                .WithId("DragParent")
                .AsTransient();
        }
        void BindCellTemplate()
        {
            Container.BindInstance(CellTemplate)
                .WithId("CellTemplate")
                .AsTransient();
            Container.BindInstance(EmptyCellTemplate)
                .WithId("EmptyCellTemplate")
                .AsTransient();
        }
        void BindInventories()
        {
            Container.Bind<InventoryBase>().To<InventoryStorage>()
                .FromComponentInHierarchy()
                .AsTransient();

            Container.BindInstance(_storageInventoryInfo)
                .WithId("InventoryStorageInfo")
                .WhenInjectedInto<InventoryStorage>();

            Container.BindInstance(_activeInventoryInfo)
                .WithId("ActiveInventoryInfo")
                .WhenInjectedInto<ActiveInventory>();

            Container.Bind<InventoryStorage>()
                .To<ActiveInventory>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<Pack>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
        void BindFactories()
        {
            Container.Bind<IInventoryCellFactory>()
                .To<StorageCellFactory>()
                .WithArguments(CellTemplate, EmptyCellTemplate)
                .WhenInjectedInto<InventoryStorage>();
        }
        void BindItemResourceLogic()
        {
            Container.Bind<ItemResourceDroper>().AsSingle();
            Container.BindInstance(ItemSourceSO).AsTransient();
            Container.BindInstance(ItemResourcePrefab).AsTransient();
            Container.Bind<ItemResource>().AsTransient();
            Container.Bind<IItemResourceFactory>()
                .To<ItemResourceFactory>()
                .WithArguments(Container, ItemResourcePrefab)
                .WhenInjectedInto<ItemResourceDroper>();
        }
        void BindCells()
        {
            Container.Bind<InventoryCell>()
                .FromComponentInHierarchy()
                .AsTransient();
        }
    }
}
