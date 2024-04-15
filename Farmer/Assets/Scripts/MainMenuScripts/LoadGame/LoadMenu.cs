using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Scripts.MainMenuCode
{
    public class LoadMenu
    { 
        IGameStatePanelFactory _gameStatePanelFactory;
        GameObject _loadLevelsPanel;
        GameDataSaveLoader _gameDataSaveLoader;
        Transform _content;
        public LoadMenu(IGameStatePanelFactory gameStatePanelFactory,
             [Inject(Id = "LoadMenuPanel")] GameObject loadLevelsPanel,
             [Inject(Id = "LoadMenuContent")] Transform content)
    
        {
            _gameStatePanelFactory = gameStatePanelFactory;
            _loadLevelsPanel = loadLevelsPanel;
            _gameDataSaveLoader = new GameDataSaveLoader();
            _content = content;
            _loadLevelsPanel.gameObject.SetActive(false);
        }

        public void Activate()
        {
            if(_loadLevelsPanel.activeSelf == false)
            {
                _loadLevelsPanel.SetActive(true);
            }

            List<string> lvlNames = _gameDataSaveLoader.LoadWorldNamesJson();

            if(lvlNames != null &&
                lvlNames.Count > 0)
            {
                CreateStatePanels(lvlNames);
            }
        }
        void CreateStatePanels(List<string> names)
        {
            foreach(string name in names)
            {
                _gameStatePanelFactory.Create(name, _content);
            }
        }

        public void OnBack()
        {
            foreach (Transform state in _content)
            {
                GameObject.Destroy(state.gameObject);
            }
            _loadLevelsPanel.gameObject.SetActive(false);
        }
    }
}
