using Assets.Scripts;
using Scripts.FarmGameEvents;
using Scripts.MainMenuCode;
using Scripts.MouseHandle;
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
        SettingsMenu _settingsMenu;
        MouseCursor _mouseCursor;
        [Inject]
        public void Construct(InputService inputService,
            GameDataState gameDataState,
            SettingsMenu settingMenu,
            GameDataSaveLoader gameDataSaveLoader,
             MouseCursor mouseCursor)
        {
            _inputService = inputService;
            _gameDataState = gameDataState;
            _settingsMenu = settingMenu;
            _gameDataSaveLoader = gameDataSaveLoader;
            _mouseCursor = mouseCursor;
        }

        private void Start()
        {
            if (_menuPanel.activeSelf == true)
            {
                _menuPanel.SetActive(false);
            }
            if(LoadedData.Instance() != null &&
                LoadedData.IsSettingsDataDefault == false)
            {
                _settingsMenu.Load(LoadedData.Instance().SettingsData);
            }
            _settingsMenu.InitializeKeyBindings();
        }
        public void OnContinue()
        {
            if (_menuPanel.activeSelf)
            {
                _inputService.UnlockGamePlayControls();
                _menuPanel.SetActive(false);
                _mouseCursor.ChangeCursor(CursorType.Default);
            }
        }
        private void Update()
        {
            if (_inputService.IsOpenCloseMenu())
            {
                if (_menuPanel.activeSelf == false)
                {
                    _inputService.LockGamePlayControls();
                    _menuPanel.SetActive(true);
                    _mouseCursor.ChangeCursor(CursorType.Menu);
                    return;
                }
                if (_menuPanel.activeSelf == true)
                {
                    _inputService.UnlockGamePlayControls();
                    _menuPanel.SetActive(false);
                    _mouseCursor.ChangeCursor(CursorType.Default);
                    return;
                }
            }
            if(Input.GetMouseButtonDown(0))
            {
                _mouseCursor.ImitateClick();
            }
        }
        public void OnSettings()
        {
            _inputService.LockMenuControls();
            _settingsMenu.Activate();
            _menuPanel.SetActive(false);
        }
        public void OnBackFromSettings()
        {
            _inputService.UnlockMenuControls();
            _settingsMenu.Back();
            _menuPanel.SetActive(true);
        }
        public void OnExit()
        {
            GameEvents.InvokeExitTheGameEvent();
            
            _gameDataSaveLoader.SaveGameState(_gameDataState);

            _settingsMenu.Save();
            SceneManager.LoadScene(GameConfiguration.MainMenuSceneName);
        }
    }
}
