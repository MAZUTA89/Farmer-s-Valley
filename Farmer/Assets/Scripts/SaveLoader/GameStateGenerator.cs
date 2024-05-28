using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using Scripts.MainMenuCode;
using Scripts.PlacementCode;
using Scripts.PlayerCode;
using Scripts.SaveLoader;
using Scripts.SO.InteractableObjects;
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
        Dictionary<string, SeedSO> _seedSODictionary;
        FactoriesProvider _interactableObjectsFactoryProvider;
        IChestFactory _chestFactory;
        ISeedFactory _seedFactory;
        ISandFactory _sandFactory;
        ISeedFactory _treeFactory;
        ItemPlacementMap _placementMap;
        ItemPlacementMap _seedPlacementMap;
        SandTilePlacementMap _sandTilePlacementMap;
        Player _player;
        InventoryItemDataBase _inventoryItemDataBase;
        PlacementService _placementService;

        [Inject]
        public void Construct(GameDataState gameDataState,
            //Dictionary<string, IInventoryItem> inventoryItemsDictionary,
            Dictionary<string, SeedSO> seedSODictionary,
            PlacementMapsContainer placementMapsContainer,
            FactoriesProvider interactableObjectsFactoryProvider,
            InventoryItemDataBase inventoryItemDataBase,
            PlacementService placementService,
            Player player)
        {
            _interactableObjectsFactoryProvider = interactableObjectsFactoryProvider;
            _seedSODictionary = seedSODictionary;
            _gameDataState = gameDataState;
            _inventoryItemDataBase = inventoryItemDataBase;
            //_inventoryItemsDictionary = inventoryItemsDictionary;
            _placementMap = placementMapsContainer.ItemPlacementMap;
            _seedPlacementMap = placementMapsContainer.SeedPlacementMap;
            _sandTilePlacementMap = placementMapsContainer.SandTilePlacementMap;
            _player = player;
            _placementService = placementService;
        }

        private void Start()
        {
            _chestFactory =
                (ChestFactory)_interactableObjectsFactoryProvider.GetFactory<ChestFactory>();
            _seedFactory =
                (SeedFactory)_interactableObjectsFactoryProvider.GetFactory<SeedFactory>();
            _sandFactory =
                (SandFactory)_interactableObjectsFactoryProvider.GetFactory<SandFactory>();
            _treeFactory =
                (OakSeedFactory)_interactableObjectsFactoryProvider.GetFactory<OakSeedFactory>();
            LoadPlayerInventories();
            LoadPlacementItems();
            LoadPlayerData();
        }
        void LoadPlayerData()
        {
            PlayerData playerData = _gameDataState.PlayerData;
            if (LoadedData.IsGameStateDefault == false)
            {
                _player.Load(playerData);
            }
        }
        void LoadPlacementItems()
        {
            if (LoadedData.IsGameStateDefault == true)
                return;


            _placementService.Load(); 
            
            List<PlacementItemData> placementItems =
                _gameDataState.PlacementObjectsDataList;

            foreach (PlacementItemData itemData in placementItems)
            {
                switch (itemData)
                {
                    case SandData data:
                        {
                            Vector3Int sandPos = data.GetPosition();
                            _sandFactory.Create(sandPos);
                            _sandTilePlacementMap.AddPosition(sandPos);
                            break;
                        }
                    case ChestData data:
                        {
                            List<InventoryItem> inventoryItems = ProcessInventoryItemsData(data.Items);
                            Chest chest = _chestFactory.Create(inventoryItems);
                            Vector3Int pos3 = data.GetPosition();
                            _placementMap.PlaceObjectOnCell(chest.gameObject, pos3);
                            _placementMap.AddPosition(pos3);
                            break;
                        }
                    case TreeData data:
                        {
                             Seed tree = _treeFactory.Create(_seedSODictionary[data.SeedSOName]);
                            _placementMap.AddPosition(data.GetPosition());
                            _placementMap.PlaceObjectOnCell(tree.gameObject, data.GetPosition());
                            tree.LoadSeed(data);
                            break;
                        }
                    case SeedData data:
                        {
                            Seed seed = _seedFactory.Create(_seedSODictionary[data.SeedSOName]);
                            _seedPlacementMap.PlaceObjectOnCell(seed.gameObject, data.GetPosition());
                            _seedPlacementMap.AddPosition(data.GetPosition());
                            seed.LoadSeed(data);
                            break;
                        }
                    
                }
            }

        }
        void LoadPlayerInventories()
        {
            if (LoadedData.IsGameStateDefault)
            {
                _backPackInventory.Initialize(CopyItemList(_backPackStartItemKit));
                _activeInventory.Initialize(CopyItemList(_activeStartItemKit));
                
            }
            else
            {
                var backPackItems = ProcessInventoryItemsData(_gameDataState.BackPackInventory);
                var activePackItems = ProcessInventoryItemsData(_gameDataState.ActivePackInventory);
                _backPackInventory.Initialize(backPackItems);
                _activeInventory.Initialize(activePackItems);
            }
        }
        List<InventoryItem> CopyItemList(List<InventoryItem> items)
        {
            List<InventoryItem> inventoryItems = new List<InventoryItem>();

            foreach (InventoryItem item in items)
            {
                InventoryItem copy;
                copy = (InventoryItem)item.Clone();
                inventoryItems.Add(copy);
            }

            return inventoryItems;
        }
        private InventoryItem ProcessItemData(InventoryItemData itemData)
        {
            string itemName = itemData.SoName;
            InventoryItem item = (InventoryItem)_inventoryItemDataBase.GetItemByName(itemName)
                .Clone();
            item.Count = itemData.Amount;
            switch(item)
            {
                case HoeItem hoeItem:
                        return hoeItem;
                case WateringItem wateringItem:
                    return wateringItem;
                case SeedBagItem seedBagItem:
                    return seedBagItem;
                case ProductItem productItem:
                    return productItem;
                case BasketItem basketItem:
                    return basketItem;
                default: return item;
            }
            
        }
        private List<InventoryItem> ProcessInventoryItemsData(
            List<InventoryItemData> inventoryItemsData)
        {
            List<InventoryItem> inventoryItems
                = new List<InventoryItem>();
            foreach (var itemData in inventoryItemsData)
            {
                inventoryItems.Add(ProcessItemData(itemData));
            }
            return inventoryItems;
        }
    }
}
