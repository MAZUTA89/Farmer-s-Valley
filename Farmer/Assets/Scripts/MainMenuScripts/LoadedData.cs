using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.MainMenuCode
{
    public class LoadedData : MonoBehaviour
    {
        static LoadedData _dataInstance;
        
        static LoadedData()
        {
            IsGameStateDefault = true;

        }
       
        private void Awake()
        {
            if (_dataInstance == null)
            {
                _dataInstance = this;
            }
            DontDestroyOnLoad(this);
        }
        public static bool IsGameStateDefault { get; private set; }
        public GameDataState GameDataState { get; private set; }
        public static bool IsSettingsDataDefault { get; private set; }
        public SettingsData SettingsData { get; private set; }
        public void InitializeGameStateData(GameDataState gameDataState, bool isDefault)
        {
            GameDataState = gameDataState;
            IsGameStateDefault = isDefault;
        }
        public void InitializeSettingsData(SettingsData settingsData, bool isDefault)
        {
            SettingsData = settingsData;
            IsSettingsDataDefault = isDefault;
        }
        public static LoadedData Instance()
        {
            return _dataInstance;
        }
    }
}
