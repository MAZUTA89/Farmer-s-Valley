using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MainMenuCode
{
    public class LoadedData : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        public bool IsDefault {  get; private set; }
        public GameDataState GameDataState { get; private set; }

        public void Initialize(GameDataState gameDataState, bool isDefault)
        {
            GameDataState = gameDataState;
            IsDefault = isDefault;
        }


    }
}
