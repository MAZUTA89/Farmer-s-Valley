using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AScripts.SaveLoader
{
    public class GameStateGenerator : MonoBehaviour
    {
        GameDataState _gameDataState;
        [Inject]
        public void Construct(GameDataState gameDataState
            )
        {
            _gameDataState = gameDataState;
        }

        private void Start()
        {
           
        }


    }
}
