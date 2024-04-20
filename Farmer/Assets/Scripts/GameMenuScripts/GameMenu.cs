using Assets.Scripts;
using Scripts.FarmGameEvents;
using Scripts.MainMenuCode;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;


namespace Scripts.GameMenuCode
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] GameObject _menuPanel;
        InputService _inputService;
        GameDataState _gameDataState;
        GameDataSaveLoader _gameDataSaveLoader;

        [Inject]
        public void Construct(InputService inputService,
            GameDataState gameDataState,
            GameDataSaveLoader gameDataSaveLoader)
        {
            _inputService = inputService;
            _gameDataState = gameDataState;
            _gameDataSaveLoader = gameDataSaveLoader;
        }

        private void Start()
        {
            if (_menuPanel.activeSelf == true)
            {
                _menuPanel.SetActive(false);
            }
        }
        public void OnContinue()
        {
            if (_menuPanel.activeSelf)
            {
                _menuPanel.SetActive(false);
            }
        }
        private void Update()
        {
            if (_inputService.IsOpenCloseMenu())
            {
                if (_menuPanel.activeSelf == false)
                {
                    _menuPanel.SetActive(true);
                    return;
                }
                if (_menuPanel.activeSelf == true)
                {
                    _menuPanel.SetActive(false);
                    return;
                }
            }
        }
        public void OnExit()
        {
            GameEvents.InvokeExitTheGameEvent();
            
            _gameDataSaveLoader.SaveGameState(_gameDataState);
            SceneManager.LoadScene(GameConfiguration.MainMenuSceneName);
        }
    }
}
