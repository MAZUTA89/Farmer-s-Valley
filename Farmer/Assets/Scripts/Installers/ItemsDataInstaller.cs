using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.InventoryCode;
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
            //BindInventoryItemsDictionary();
            BindSeedDataDictionary();
        }
        void BindInventoryItemsDictionary()
        {
            Dictionary<string, InventoryItem> InventoryItemsDictionary
                = new Dictionary<string, InventoryItem>();

            foreach (var item in _inventoryItemAssetList)
            {
                InventoryItemsDictionary[item.DisplayName] = item;
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
    }
}
