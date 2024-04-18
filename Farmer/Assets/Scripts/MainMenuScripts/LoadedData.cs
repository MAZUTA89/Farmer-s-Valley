using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MainMenuCode
{
    public class LoadedData : MonoBehaviour
    {
        static LoadedData _dataInstance;
        private void Awake()
        {
            if (_dataInstance == null)
            {
                _dataInstance = this;
            }
            DontDestroyOnLoad(this);
        }
        public bool IsDefault { get; private set; }
        public GameDataState GameDataState { get; private set; }

        public void Initialize(GameDataState gameDataState, bool isDefault)
        {
            GameDataState = gameDataState;
            IsDefault = isDefault;
        }
        public static LoadedData Instance()
        {
            return _dataInstance;
        }
    }
}
