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
            if(LoadedData.Instance().IsDefault)
            {
                _gameDataState = LoadedData.Instance().GameDataState;
                foreach (var placementItem in _placementItems)
                {
                    ISaveLoadItem saveLoadItem = placementItem as ISaveLoadItem;
                    _gameDataState.AddItemData(saveLoadItem.GetData());
                }
            }
        }
    }
}
