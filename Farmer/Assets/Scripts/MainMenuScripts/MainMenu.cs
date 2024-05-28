using Scripts.FarmGameEvents;
using Scripts.MouseHandle;
using Scripts.SaveLoader;
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
        MouseCursor _mouseCursor;
        [Inject]
        public void Construct(LoadMenu loadMenu,
            NewGameMenu newGameMenu,
            SettingsMenu settingsMenu,
            [Inject(Id = "MainMenuObject")] GameObject menuObject,
            MouseCursor mouseCursor)
        {
            _mouseCursor = mouseCursor;
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
            _settingsMenu.InitializeKeyBindings();

        }
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                _mouseCursor.ImitateClick();
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
