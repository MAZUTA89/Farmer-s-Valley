using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using Scripts.MainMenuCode;
using Scripts.PlacementCode;
using Scripts.PlayerCode;
using Scripts.SaveLoader;
using Scripts.SO.InteractableObjects;
using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
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
        InteractableObjectsFactoryProvider _interactableObjectsFactoryProvider;
        IChestFactory _chestFactory;
        ISeedFactory _seedFactory;
        ISandFactory _sandFactory;
        ISeedFactory _treeFactory;
        ItemPlacementMap _placementMap;
        ItemPlacementMap _seedPlacementMap;
        SandTilePlacementMap _sandTilePlacementMap;
        Player _player;


        [Inject]
        public void Construct(GameDataState gameDataState,
            Dictionary<string, IInventoryItem> inventoryItemsDictionary,
            Dictionary<string, SeedSO> seedSODictionary,
            PlacementMapsContainer placementMapsContainer,
            InteractableObjectsFactoryProvider interactableObjectsFactoryProvider,
            Player player)
        {
            _interactableObjectsFactoryProvider = interactableObjectsFactoryProvider;
            _seedSODictionary = seedSODictionary;
            _gameDataState = gameDataState;
            _inventoryItemsDictionary = inventoryItemsDictionary;
            _placementMap = placementMapsContainer.ItemPlacementMap;
            _seedPlacementMap = placementMapsContainer.SeedPlacementMap;
            _sandTilePlacementMap = placementMapsContainer.SandTilePlacementMap;
            _player = player;
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
            if (LoadedData.IsDefault == true)
                return;
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
                            List<IInventoryItem> inventoryItems = ProcessInventoryItemsData(data.Items);
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
