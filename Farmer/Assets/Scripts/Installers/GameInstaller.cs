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
using Zenject;

namespace Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
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
        public override void InstallBindings()
        {
            BindInputService();
            BindGameMenu();
            BindGameDataState();
            BindPlayer();
            BindItemsUsage();
            BindChest();
            BindInteractableObjectsFactories();
        }
        void BindInputService()
        {
            Container.Bind<InputService>().AsSingle();
        }
        void BindGameMenu()
        {
            Container.Bind<GameMenu>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
        void BindGameDataState()
        {
            if (LoadedData.IsDefault)
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
            InteractableObjectsFactoryProvider interactableObjectsFactoryProvider
                = new InteractableObjectsFactoryProvider();
            OakSeedFactory oakSeedFactory =
                new OakSeedFactory(Container, _treeTemplate);

            interactableObjectsFactoryProvider.RegisterFabric(seedFactory);
            interactableObjectsFactoryProvider.RegisterFabric(sandFactory);
            interactableObjectsFactoryProvider.RegisterFabric(chestFactory);
            interactableObjectsFactoryProvider.RegisterFabric(oakSeedFactory);

            Container.BindInstance(interactableObjectsFactoryProvider).AsSingle();

            //ISeedFactory seedFactory1 =
            //    (ISeedFactory)interactableObjectsFactoryProvider
            //    .GetFactory<SeedFactory>();
        }
    }
}

