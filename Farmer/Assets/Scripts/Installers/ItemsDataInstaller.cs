using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.SO.InventoryItem;

namespace Scripts.Installers
{
    public class ItemsDataInstaller : MonoInstaller
    {
        [Header("Все предметы инвентаря в игре")]
        [SerializeField] List<InventoryItemAssetData> _inventoryItemAssetList;
        public override void InstallBindings()
        {
            Dictionary<string, InventoryItemAssetData> InventoryItemsDictionary
                = new Dictionary<string, InventoryItemAssetData>();

            foreach (var item in _inventoryItemAssetList)
            {
                InventoryItemsDictionary[item.Name] = item;
            }

            Container.BindInstance(InventoryItemsDictionary).AsSingle();
        }
    }
}
