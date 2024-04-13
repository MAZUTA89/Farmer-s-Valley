using Scripts.ChestItem;
using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.InventoryCode;

namespace Scripts.Installers
{
    public class ChestInstaller : MonoInstaller
    {
        [SerializeField] private ChestInventory InventoryStorageTemplate;
        public override void InstallBindings()
        {
            //Container.BindInstance(InventoryStorageTemplate)
            //    .WithId("StoragePanelTemplate");
            //InventoryStoragePanelFactory inventoryStoragePanelFactory
            //    = new InventoryStoragePanelFactory(Container, InventoryStorageTemplate);
            //Container.BindInstance(inventoryStoragePanelFactory);
            Container.Bind<IInventoryPanelFactory>()
                .To<InventoryChestPanelFactory>()
                .WithArguments(Container, InventoryStorageTemplate)
                .WhenInjectedInto<Chest>();

            Container.Bind<Chest>().FromComponentInHierarchy().AsTransient();
        }
    }
}
