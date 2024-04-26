using Scripts.GameMenuCode;
using Scripts.InteractableObjects;
using Scripts.ItemUsage;
using Scripts.MainMenuCode;
using Scripts.PlacementCode;
using Scripts.PlayerCode;
using Scripts.SaveLoader;
using Scripts.SO.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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
        public override void InstallBindings()
        {
            BindInputService();
            BindGameMenu();
            BindGameDataState();
            BindPlayer();
            BindItemsUsage();
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

            Container.BindInstance(placementMapsContainer).AsSingle();

            Container.Bind<PlacementItem>().To<Chest>().AsTransient();
            Container.Bind<PlacementItem>().To<Seed>().AsTransient();

            Container.Bind<MapClicker>().AsSingle();
            Container.Bind<ItemApplier>().AsSingle();
        }
    }
}

