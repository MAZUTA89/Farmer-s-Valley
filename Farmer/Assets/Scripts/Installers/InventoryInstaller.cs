using Scripts.InventoryCode;
using Scripts.SO.InventoryItem;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Installers
{
    public class InventoryInstaller : MonoInstaller
    {
        [Header("Canvas")]
        [SerializeField] Transform DragParent;
        [Header("Active inventory")]
        [SerializeField] private Inventory _activeInventory;
        [Space]
        [Space]
        [Header("Backpack inventory")]
        [SerializeField] private Inventory _backPackInventory;
        [SerializeField] List<InventoryItemAssetData> StartKit;
        public override void InstallBindings()
        {
            _backPackInventory.InitializeItemKit(StartKit);
            _activeInventory.InitializeItemKit(new List<InventoryItemAssetData>());
            Container.Bind<Inventory>().FromComponentInHierarchy().AsTransient();
            Container.BindInstance(DragParent)
                .WithId("DragParent")
                .AsSingle();
            //Container.BindInstance(_backPackInventory)
            //    .WithId("BackpackInventory")
            //    .AsSingle();
            //Container.BindInstance(_activeInventory)
            //    .WithId("ActiveInventory")
            //    .AsSingle();
            //Container.Bind<PlayerInventory>().FromComponentInHierarchy().AsSingle();

            //Container.Bind<Inventory>().WithArguments(InventoryContainer, StartKit).WhenInjectedInto<PlayerInventory>();


        }
    }
}
