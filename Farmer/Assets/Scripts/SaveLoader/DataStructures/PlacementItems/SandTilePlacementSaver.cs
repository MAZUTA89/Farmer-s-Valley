using AScripts.SaveLoader;
using Scripts.FarmGameEvents;
using Scripts.PlacementCode;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.SaveLoader
{
    public class SandTilePlacementSaver : MonoBehaviour
    {
        SandTilePlacementMap _map;
        GameDataState _gameDataState;
        [Inject]
        public void Construct(PlacementMapsContainer placementMapsContainer,
            GameDataState gameDataState)
        {
            _map = placementMapsContainer.SandTilePlacementMap;
            _gameDataState = gameDataState;
        }
        private void OnEnable()
        {
            GameEvents.OnExitTheGameEvent += OnExitTheGame;
        }
        private void OnDisable()
        {
            GameEvents.OnExitTheGameEvent -= OnExitTheGame;
        }
        public void OnExitTheGame()
        {
            foreach (var position in _map.OccupiedPositions)
            {
                SandData sandData = new SandData();
                sandData.SetPosition(position);
                _gameDataState.AddItemData(sandData);
            }
        }
    }
}
