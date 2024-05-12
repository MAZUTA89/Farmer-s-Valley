using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.MainMenuCode;
using Scripts.MainMenuScripts;
using Scripts.SaveLoader;

namespace Scripts.Installers
{
    class MainMenuInstaller : MonoInstaller
    {
        [Header("Главное меню")]
        [SerializeField] private GameObject _menuObject;
        [Header("Меню загрузки")]
        [SerializeField] private GameStatePanel _statePanelTemplate;
        [SerializeField] private GameObject _loadMenuPanel;
        [SerializeField] private Transform _content;
        [Header("Меню создания")]
        [SerializeField] private NewGamePanel _newGamePanel;
        [Header("Меню настроек")]
        [SerializeField] private SettingsPanel _settingsPanel;
        public override void InstallBindings()
        {
            BindMainMenu();
            BindLoadMenu();
            BindSettingsMenu();
            BindNewGameMenu();
        }
        void BindMainMenu()
        {
            Container.BindInstance(_menuObject)
                .WithId("MainMenuObject")
                .AsTransient();
            Container.Bind<MainMenu>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
        void BindLoadMenu()
        {
            Container.BindInstance(_statePanelTemplate)
                .WithId("StatePanelTemplate").AsTransient();
            Container.BindInstance(_loadMenuPanel).WithId("LoadMenuPanel")
                .AsTransient();
            Container.BindInstance(_content).WithId("LoadMenuContent")
                .AsTransient();
            Container.Bind<IGameStatePanelFactory>()
                .To<StatePanelFactory>()
                .WhenInjectedInto<LoadMenu>();
            Container.Bind<LoadMenu>().AsSingle();
        }
        void BindNewGameMenu()
        {
            Container.BindInstance(_newGamePanel)
                .WithId("NewGamePanel")
                .AsTransient();
            Container.Bind<NewGameMenu>().AsSingle();
        }
        void BindSettingsMenu()
        {
            SettingsMenu settingsMenu = new SettingsMenu(_settingsPanel);
            
            Container.BindInstance(settingsMenu).AsSingle();
            Container.Bind<MenuSound>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
        void BindSatePanel()
        {
            Container.Bind<GameStatePanel>().AsTransient();
        }
    }
}
