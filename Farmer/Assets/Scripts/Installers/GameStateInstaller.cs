using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.PlacementCode;
using Scripts.SaveLoader;
using Scripts.MainMenuCode;

namespace Scripts.Installers
{
    public class GameStateInstaller : MonoInstaller
    {
        [Header("Дефолтные объекты на уровне")]
        [SerializeField] private List<PlacementItem> _placementItems;
        GameDataState _gameDataState;

        public override void InstallBindings()
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
    }
}
