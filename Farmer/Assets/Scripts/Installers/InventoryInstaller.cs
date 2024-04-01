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
        [SerializeField] List<InventoryItemSO> StartKit;
        [SerializeField] Transform InventoryContainer;
        [SerializeField] Transform DragParent;
        public override void InstallBindings()
        {
            Container.Bind<PlayerInventory>().FromComponentInHierarchy().AsSingle();
            
            Inventory playerInventory = new Inventory(InventoryContainer, DragParent, StartKit);
            Container.BindInstance(playerInventory).WhenInjectedInto<PlayerInventory>();
            //Container.Bind<Inventory>().WithArguments(InventoryContainer, StartKit).WhenInjectedInto<PlayerInventory>();


        }
    }
}
