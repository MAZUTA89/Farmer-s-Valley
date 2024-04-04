using Scripts.InventoryCode;
using Scripts.SO.Inventory;
using Scripts.SO.InventoryItem;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        [Header("Active inventory")]
        [SerializeField] private InventoryBase _activeInventory;
        [SerializeField] private InventoryInfo _activeInventoryInfo;
        [Space]
        [Space]
        [Header("Backpack inventory")]
        [SerializeField] private InventoryInfo _storageInventoryInfo;
        [SerializeField] private InventoryBase _backPackInventory;
        [SerializeField] List<InventoryItemAssetData> StartKit;
        public override void InstallBindings()
        {
            _backPackInventory.Initialize(StartKit);
            _activeInventory.Initialize(new List<InventoryItemAssetData>());
            BindInventories();
            BindCellTemplate();
            BindGlobalVisualContext();
            //Container.BindInstance(_backPackInventory)
            //    .WithId("BackpackInventory")
            //    .AsSingle();
            //Container.BindInstance(_activeInventory)
            //    .WithId("ActiveInventory")
            //    .AsSingle();
            //Container.Bind<PlayerInventory>().FromComponentInHierarchy().AsSingle();

            //Container.Bind<Inventory>().WithArguments(InventoryContainer, StartKit).WhenInjectedInto<PlayerInventory>();


        }
        void BindGlobalVisualContext()
        {
            Container.BindInstance(DragParent)
                .WithId("DragParent")
                .AsSingle();
        }
        void BindCellTemplate()
        {
            Container.BindInstance(CellTemplate)
                .WithId("CellTemplate")
                .AsTransient();
        }
        void BindInventories()
        {
            Container.Bind<InventoryBase>().AsTransient();
            Container.Bind<InventoryStorage>().AsTransient();
            Container.BindInstance(_storageInventoryInfo)
                .WithId("InventoryStorageInfo")
                .AsTransient();
            Container.Bind<ActiveInventory>().AsSingle();
            Container.BindInstance(_activeInventoryInfo)
                .WithId("ActiveInventoryInfo")
                .AsTransient();
        }
    }
}
