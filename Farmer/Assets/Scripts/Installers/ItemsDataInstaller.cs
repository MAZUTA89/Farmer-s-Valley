using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.InventoryCode;
using Assets.Scripts.Inventory.Items;

namespace Scripts.Installers
{
    public class ItemsDataInstaller : MonoInstaller
    {
        [Header("Все предметы инвентаря в игре")]
        [SerializeField] List<InventoryItem> _inventoryItemAssetList;

        
        public override void InstallBindings()
        {
            BindInventoryItemsDictionary();
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
    }
}
