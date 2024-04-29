using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using Scripts.MainMenuCode;
using Scripts.PlacementCode;
using Scripts.PlayerCode;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AScripts.SaveLoader
{
    public class GameStateGenerator : MonoBehaviour
    {
        [Header("Back Pack")]
        [SerializeField] List<InventoryItem> _backPackStartItemKit;
        [SerializeField] InventoryBase _backPackInventory;
        [Space]
        [Space]
        [Header("Active Pack")]
        [SerializeField] List<InventoryItem> _activeStartItemKit;
        [SerializeField] InventoryBase _activeInventory;
        [Space]
        [Space]
        [Header("Seller Pack")]
        [SerializeField] List<InventoryItem> _sellerStartItemKit;
        [SerializeField] InventoryBase _sellerInventory;

        GameDataState _gameDataState;
        Dictionary<string, IInventoryItem> _inventoryItemsDictionary;
        IChestFactory _chestFactory;
        ItemPlacementMap _placementMap;
        Player _player;


        [Inject]
        public void Construct(GameDataState gameDataState,
            Dictionary<string, IInventoryItem> inventoryItemsDictionary,
            PlacementMapsContainer placementMapsContainer,
            IChestFactory chestFactory,
            Player player)
        {
            _gameDataState = gameDataState;
            _inventoryItemsDictionary = inventoryItemsDictionary;
            _chestFactory = chestFactory;
            _placementMap = placementMapsContainer.ItemPlacementMap;
            _player = player;
        }

        private void Start()
        {
            LoadPlayerInventories();
            LoadPlacementItems();
            LoadPlayerData();
            //LoadChestData();
        }
        void LoadPlayerData()
        {
            PlayerData playerData = _gameDataState.PlayerData;
            if (LoadedData.IsDefault == false)
            {
                _player.Load(playerData);
            }
        }
        void LoadPlacementItems()
        {
            List<PlacementItemData> placementItems =
                _gameDataState.PlacementObjectsDataList;

            foreach (PlacementItemData itemData in placementItems)
            {
                switch (itemData)
                {
                    case SandData data:
                        {
                            Vector3Int sandPos = data.GetPosition();

                            break;
                        }
                    case ChestData data:
                        {
                            List<IInventoryItem> inventoryItems = ProcessInventoryItemsData(data.Items);
                            Chest chest = _chestFactory.Create(inventoryItems);
                            Vector3Int pos3 = itemData.GetPosition();
                            _placementMap.PlaceObjectOnCell(chest.gameObject, pos3);
                            _placementMap.AddPosition(pos3);
                            break;
                        }
                    case SeedData data:
                        {
                            break;
                        }
                }
                if (itemData is ChestData)
                {
                    
                }
            }

        }
        void LoadPlayerInventories()
        {
            if (LoadedData.IsDefault)
            {
                List<IInventoryItem> activeItems = new List<IInventoryItem>(_activeStartItemKit);
                List<IInventoryItem> backItems = new List<IInventoryItem>(_backPackStartItemKit);
                _backPackInventory.Initialize(backItems);
                _activeInventory.Initialize(activeItems);
            }
            else
            {
                List<IInventoryItem> backPackInventoryItems =
                    ProcessInventoryItemsData(_gameDataState.BackPackInventory);
                _backPackInventory.Initialize(backPackInventoryItems);

                List<IInventoryItem> activePackInventoryItems =
                    ProcessInventoryItemsData(_gameDataState.ActivePackInventory);
                _activeInventory.Initialize(activePackInventoryItems);
            }
        }

        private IInventoryItem ProcessItemData(InventoryItemData itemData)
        {
            string itemName = itemData.SoName;
            IInventoryItem inventoryItem =
                _inventoryItemsDictionary[itemName];
            switch (itemData)
            {
                case HoeInventoryItemData data:
                    {
                        IHoeInventoryItem hoe = inventoryItem as IHoeInventoryItem;
                        return hoe;
                    }
                case OakBagInventoryItemData data:
                    {
                        IOakBagInventoryItem oakBag = inventoryItem as IOakBagInventoryItem;
                        oakBag.Count = data.Count;
                        return oakBag;
                    }
                case BagInventoryItemData data:
                    {
                        IBagInventoryItem bag = inventoryItem as IBagInventoryItem;
                        bag.Count = data.Count;
                        return bag;
                    }
                case ChestInventoryItemData data:
                    {
                        IChestInventoryItem chest = inventoryItem as IChestInventoryItem;
                        List<IInventoryItem> inventoryItems
                            = ProcessInventoryItemsData(data.ItemsList);
                        chest.Items = inventoryItems;
                        return chest;
                    }
                case QuantitativeItemData data:
                    {
                        IQuantitativeInventoryItem quantitativeInventoryItem
                            = inventoryItem as IQuantitativeInventoryItem;
                        quantitativeInventoryItem.Count = data.Count;
                        return quantitativeInventoryItem;
                    }
                
                default:
                    {
                        return inventoryItem;
                    }
            }
        }
        private List<IInventoryItem> ProcessInventoryItemsData(
            List<InventoryItemData> inventoryItemsData)
        {
            List<IInventoryItem> inventoryItems
                = new List<IInventoryItem>();
            foreach (var itemData in inventoryItemsData)
            {
                inventoryItems.Add(ProcessItemData(itemData));
            }
            return inventoryItems;
        }
    }
}
