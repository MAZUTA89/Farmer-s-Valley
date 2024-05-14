using PimDeWitte.UnityMainThreadDispatcher;
using Scripts.FarmGameEvents;
using Scripts.MainMenuScripts;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Scripts.MainMenuCode
{
    public class MainMenu : MonoBehaviour
    {
        private const string GameSceneName = "FarmScene";
        private LoadMenu _loadMenu;
        private NewGameMenu _newGameMenu;
        private GameObject _menuObject;
        private SettingsMenu _settingsMenu;

        [Inject]
        public void Construct(LoadMenu loadMenu,
            NewGameMenu newGameMenu,
            SettingsMenu settingsMenu,
            [Inject(Id = "MainMenuObject")] GameObject menuObject)
        {
            _loadMenu = loadMenu;
            _newGameMenu = newGameMenu;
            _settingsMenu = settingsMenu;
            _menuObject = menuObject;
        }
        private void OnEnable()
        {
            GameEvents.OnSaveSettingsEvent += _settingsMenu.Save;
        }
        private void OnDisable()
        {
            GameEvents.OnSaveSettingsEvent -= _settingsMenu.Save;
        }
        private void Start()
        {
            _settingsMenu.InitializeKeyBindings();
            SettingsDataSaveLoader settingsDataSaveLoader = new();
            SettingsData settingsData = null;
            if (settingsDataSaveLoader.IsDefault())
            {
                settingsData = new SettingsData();
                LoadedData.Instance().InitializeSettingsData(settingsData, true);
            }
            else
            {
                settingsData = settingsDataSaveLoader.Load();
                LoadedData.Instance().InitializeSettingsData(settingsData, false);
            }
            _menuObject.SetActive(true);
            if(LoadedData.IsSettingsDataDefault == false)
            {
                _settingsMenu.Load(settingsData);
            }
        }
        public void NewGame()
        {
            _menuObject.SetActive(false);
            _newGameMenu.Activate();
        }
        public void LoadGame()
        {
            _menuObject.SetActive(false);
            _loadMenu.Activate();
        }
        public void Settings()
        {
            _settingsMenu.Activate();
            _menuObject.SetActive(false);
        }

        public void WorkSpace()
        {
            _settingsMenu.Save();
            Application.Quit();
        }
        
        public void OnBack()
        {
            _loadMenu.OnBack();
            _menuObject.SetActive(true);
        }
        public void Create()
        {
            _newGameMenu.OnCreate();
        }
        public void BackFromCreate()
        {
            _newGameMenu.OnBack();
            _menuObject.SetActive(true);
        }
        public void BackFromSettings() 
        {
            _settingsMenu.Back();
            _menuObject.SetActive(true);
        }

       
    }
}
