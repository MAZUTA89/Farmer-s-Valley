using Scripts.GameMenuCode;
using Scripts.MainMenuCode;
using Scripts.PlacementCode;
using Scripts.PlayerCode;
using Scripts.SaveLoader;
using Scripts.SO.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
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
        public override void InstallBindings()
        {
            BindInputService();
            BindGameMenu();
            BindGameDataState();
            BindPlayer();
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
    }
}

