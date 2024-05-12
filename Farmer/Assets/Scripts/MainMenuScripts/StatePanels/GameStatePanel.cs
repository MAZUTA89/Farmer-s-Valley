using Assets.Scripts;
using Scripts.FarmGameEvents;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.MainMenuCode
{
    public class GameStatePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI NameTextField;
        public TextMeshProUGUI NameText => NameTextField;

        GameDataSaveLoader _gameDataSaveLoader;

        private void Start()
        {
            _gameDataSaveLoader = new GameDataSaveLoader();
        }

        public void Load()
        {
            GameDataState gameDataState = 
                _gameDataSaveLoader.LoadGameState(NameText.text);
            LoadedData.Instance().InitializeGameStateData(gameDataState, false);
            GameEvents.InvokeOnSaveSettingsEvent();
            SceneManager.LoadScene(GameConfiguration.FarmSceneName);
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
