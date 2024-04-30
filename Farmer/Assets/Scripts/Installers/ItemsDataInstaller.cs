using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.InventoryCode;
using Assets.Scripts.Inventory.Items;
using Scripts.SO.InteractableObjects;

namespace Scripts.Installers
{
    public class ItemsDataInstaller : MonoInstaller
    {
        [Header("Все предметы инвентаря в игре")]
        [SerializeField] List<InventoryItem> _inventoryItemAssetList;
        [Header("Все данные для семян:")]
        [SerializeField] List<SeedSO> _seedDataList; 
        
        public override void InstallBindings()
        {
            BindInventoryItemsDictionary();
            BindSeedDataDictionary();
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
        void BindSeedDataDictionary()
        {
            Dictionary<string, SeedSO> SeedDataDictionary
                = new Dictionary<string, SeedSO>();

            foreach (var seedSO in _seedDataList)
            {
                SeedDataDictionary[seedSO.name] = seedSO;
            }

            Container.BindInstance(SeedDataDictionary).AsSingle();
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
