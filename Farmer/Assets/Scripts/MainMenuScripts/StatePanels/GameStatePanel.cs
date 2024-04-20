using Assets.Scripts;
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
            LoadedData.Instance().Initialize(gameDataState, false);
            SceneManager.LoadScene(GameConfiguration.FarmSceneName);
        }
    }
}
