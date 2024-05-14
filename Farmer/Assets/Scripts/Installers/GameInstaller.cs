using HappyHarvest;
using Scripts.GameMenuCode;
using Scripts.InteractableObjects;
using Scripts.InventoryCode;
using Scripts.ItemUsage;
using Scripts.MainMenuCode;
using Scripts.PlacementCode;
using Scripts.PlayerCode;
using Scripts.SaveLoader;
using Scripts.SO.InteractableObjects;
using Scripts.SO.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using Scripts.SellBuy;
using Zenject;
using TMPro;
using Newtonsoft.Json.Bson;
using Scripts.Inventory;
using Scripts.Sounds;

namespace Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("Grid:")]
        [SerializeField] private Grid _grid;
        [Header("Player binds:")]
        [Space]
        [SerializeField] private Transform _playerTransform;
        [SerializeField] PlayerSO PlayerSO;
        [Space]
        [Header("Game state binds:")]
        [Space]
        [Header("Дефолтные объекты на уровне")]
        [SerializeField] private List<PlacementItem> _placementItems;
        GameDataState _gameDataState;
        [Space]
        [Header("Item Usage:")]
        [Space]
        [Header("Elements map:")]
        [SerializeField] private Tilemap _gameElementsMap;
        [Header("Sand map:")]
        [SerializeField] private Tilemap _sandMap;
        [Header("KursorObject:")]
        [SerializeField] private GameObject _kursorObject;
        [Space]
        [Header("InteractableObjects:")]
        [Space]
        [Header("Chest inventory:")]
        [SerializeField] private ChestInventory _chestInventoryTemplate;
        [Header("Chest object:")]
        [SerializeField] private Chest _chestTemplate;
        [Header("Sand rule tile:")]
        [SerializeField] private RuleTile _sandRuleTile;
        [Header("Seed template:")]
        [SerializeField] private Seed _seedTemplate;
        [Header("Tree template:")]
        [SerializeField] private InteractableObjects.Tree _treeTemplate;
        [Header("Crop data base:")]
        [SerializeField] private CropDataBase _cropDataBase;
        [Header("Item data base:")]
        [SerializeField] private InventoryItemDataBase _itemDataBase;
        [Header("Trading:")]
        [SerializeField] private BuyItemsDatabase _buyItemsDatabase;
        [SerializeField] private SellTradeElement  SellTradeElementTemplate;
        [SerializeField] private BuyTradeElement BuyTradeElementTemplate;
        [SerializeField] private TextMeshProUGUI MoneyText;
        [Header("Settings Panel")]
        [SerializeField] private SettingsPanel _settingsPanel;
        [SerializeField] private KeyBindingPanel keyBindingPanelTemplate;
        InputService _inputService;
        FactoriesProvider _factoryProvider;

        public override void InstallBindings()
        {
            _inputService = new InputService();
            _factoryProvider = new FactoriesProvider();
            BindInputService();
            BindGameMenu();
            BindGameDataState();
            BindPlayer();
            BindItemsUsage();
            BindChest();
            BindInteractableObjectsFactories();
            BindGrid();
            BindDataBases();
            BindTradeLogic();
            BindSounds();
            Container.BindInstance(_factoryProvider).AsSingle();
        }
        void BindSounds()
        {
            Container.Bind<SoundService>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container.Bind<StepSoundHandler>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
        void BindTradeLogic()
        {
            Container.BindInstance(_buyItemsDatabase).AsSingle();
            PlayerMoney playerMoney = new PlayerMoney(PlayerSO.StartMoney, MoneyText);
            Container.BindInstance(playerMoney).AsSingle();
            SellTradeElementFactory sellTradeElementFactory = new SellTradeElementFactory(SellTradeElementTemplate,
                Container);
            BuyTradeElementFactory buyTradeElementFactory = new BuyTradeElementFactory(BuyTradeElementTemplate, Container);
            _factoryProvider.RegisterFabric(sellTradeElementFactory);
            _factoryProvider.RegisterFabric(buyTradeElementFactory);

            Container.Bind<TradeService>().AsSingle();
            Container.Bind<TradePanel>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container.Bind<TradeElement>()
                .To<BuyTradeElement>().AsTransient();
            Container.Bind<TradeElement>()
                .To<SellTradeElement>().AsTransient();
        }
        void BindDataBases()
        {
            _itemDataBase.Init();
            _cropDataBase.Init();
            Container.BindInstance(_cropDataBase).AsSingle();
            Container.BindInstance(_itemDataBase).AsSingle();
        }
        void BindGrid()
        {
            Container.BindInstance(_grid).AsSingle();
            Container.Bind<MarkerController>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container.Bind<PlacementService>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
        void BindInputService()
        {
            Container.BindInstance(_inputService).AsSingle();
        }
        void BindGameMenu()
        {
            Container.Bind<GameMenu>()
                .FromComponentInHierarchy()
                .AsSingle();
            _factoryProvider.RegisterFabric(new KeyBindingPanelFactory(keyBindingPanelTemplate, Container));
            SettingsMenu settingsMenu = new(_settingsPanel, _inputService, true);
            Container.BindInstance(settingsMenu).AsSingle();
        }
        void BindGameDataState()
        {
            if (LoadedData.IsGameStateDefault)
            {
                if (LoadedData.Instance() == null)
                {
                    _gameDataState = new GameDataState("Editor mode");
                }
                else
                {
                    _gameDataState = LoadedData.Instance().GameDataState;
                }
            }
            else
            {
                _gameDataState = LoadedData.Instance().GameDataState;
                foreach (var placementItem in _placementItems)
                {
                    Destroy(placementItem.gameObject);
                }
            }
            Container.BindInstance(_gameDataState).AsSingle();
            Container.Bind<GameDataSaveLoader>().AsSingle();
            Container.Bind<SandTilePlacementSaver>().AsSingle();
        }
        void BindPlayer()
        {
            Container.Bind<Player>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container.BindInstance(_playerTransform)
                .WithId("PlayerTransform")
                .AsTransient();
            Container.BindInstance(PlayerSO).AsTransient();
            Container.Bind<CharacterAnimationEventHandler>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
        void BindItemsUsage()
        {
            Container.BindInstance(_gameElementsMap)
                .AsTransient();
            
            ItemPlacementMap itemPlacementMap =
                new ItemPlacementMap(_gameElementsMap);
            SandTilePlacementMap sandTilePlacementMap =
                new SandTilePlacementMap(_sandMap);
            SeedPlacementMap seedPlacementMap =
                new SeedPlacementMap(_gameElementsMap);
            PlacementMapsContainer placementMapsContainer
                = new PlacementMapsContainer(
                itemPlacementMap,
                sandTilePlacementMap,
                seedPlacementMap);

            _kursorObject.SetActive(false);

            Container.BindInstance(_kursorObject).WithId("KursorObject");

            Container.BindInstance(placementMapsContainer).AsSingle();

            Container.Bind<ItemApplierTools>().AsSingle();
            Container.Bind<PlacementItem>().To<Chest>().AsTransient();
            Container.Bind<PlacementItem>().To<Seed>().AsTransient();

            Container.Bind<MapClicker>().AsSingle();
            Container.Bind<ItemApplier>().AsSingle();
        }
        void BindChest()
        {
            Container.Bind<PlacementItem>()
                .To<Chest>()
                .FromComponentInHierarchy()
                .AsTransient();

            Container.BindInstance(_chestInventoryTemplate)
                .AsTransient();
            Container.Bind<IInventoryPanelFactory>()
                 .To<InventoryChestPanelFactory>()
                 .WhenInjectedInto<Chest>();

            IChestFactory chestFactory = new ChestFactory(Container, _chestTemplate);
            Container.BindInstance(chestFactory).AsTransient();
        }
        void BindInteractableObjectsFactories()
        {
            SeedFactory seedFactory = new SeedFactory(Container, _seedTemplate);
            SandFactory sandFactory = new SandFactory(_sandMap, _sandRuleTile);
            ChestFactory chestFactory = new ChestFactory(Container, _chestTemplate);
           
            OakSeedFactory oakSeedFactory =
                new OakSeedFactory(Container, _treeTemplate);

            _factoryProvider.RegisterFabric(seedFactory);
            _factoryProvider.RegisterFabric(sandFactory);
            _factoryProvider.RegisterFabric(chestFactory);
            _factoryProvider.RegisterFabric(oakSeedFactory);

            

            //ISeedFactory seedFactory1 =
            //    (ISeedFactory)interactableObjectsFactoryProvider
            //    .GetFactory<SeedFactory>();
        }
    }
}

