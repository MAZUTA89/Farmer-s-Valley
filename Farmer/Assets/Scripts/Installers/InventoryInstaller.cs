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
        [Header("Empty cell prefab")]
        [SerializeField] private GameObject EmptyCellTemplate;
        [Header("Active inventory")]
        [SerializeField] private InventoryBase _activeInventory;
        [SerializeField] private InventoryInfo _activeInventoryInfo;
        [Space]
        [Space]
        [Header("Backpack inventory")]
        [SerializeField] private InventoryInfo _storageInventoryInfo;
        
        [SerializeField] List<InventoryItemAssetData> StartKit;
        public override void InstallBindings()
        {
            BindInventories();
            BindCellTemplate();
            BindGlobalVisualContext();

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
            Container.BindInstance(EmptyCellTemplate)
                .WithId("EmptyCellTemplate")
                .AsTransient();
        }
        void BindInventories()
        {
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
