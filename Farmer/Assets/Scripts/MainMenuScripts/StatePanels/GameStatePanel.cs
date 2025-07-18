﻿using Assets.Scripts;
using Scripts.FarmGameEvents;
using Scripts.SaveLoader;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.MainMenuCode
{
    public class GameStatePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameTextField;
        [SerializeField] private TextMeshProUGUI _moneyTextField;

        public TextMeshProUGUI MoneyText =>_moneyTextField;
        public TextMeshProUGUI NameText => _nameTextField;

        GameDataSaveLoader _gameDataSaveLoader;
        LevelLoader _levelLoader;

        private void Start()
        {
            _gameDataSaveLoader = new GameDataSaveLoader();
        }

        public async void Load()
        {
            GameDataState gameDataState = 
                _gameDataSaveLoader.LoadGameState(NameText.text);
            LoadedData.Instance().InitializeGameStateData(gameDataState, false);
            GameEvents.InvokeOnSaveSettingsEvent();
            await _levelLoader.LoadLevel(GameConfiguration.FarmSceneName);
        }
        public void SetLevelLoader(LevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }
        public void Delete()
        {
            if(PlayerPrefs.HasKey(GameConfiguration.SaveLevelNamesKeyName))
            {
                List<string> names = _gameDataSaveLoader.LoadWorldNamesJson();
                foreach(string name in names)
                {
                    if(name == NameText.text)
                    {
                        names.Remove(name);
                        _gameDataSaveLoader.SaveWorldNamesJson(names);
                        if(PlayerPrefs.HasKey(name))
                        {
                            PlayerPrefs.DeleteKey(name);
                        }
                        Destroy(gameObject);
                        return;
                    }
                }
            }
        }
    }
}
