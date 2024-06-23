using Assets.Scripts;
using Scripts.SaveLoader;
using System.Collections.Generic;
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
        LevelLoader _levelLoader;
        public LoadMenu(IGameStatePanelFactory gameStatePanelFactory,
             [Inject(Id = "LoadMenuPanel")] GameObject loadLevelsPanel,
             [Inject(Id = "LoadMenuContent")] Transform content,
             LevelLoader levelLoader)
    
        {
            _gameStatePanelFactory = gameStatePanelFactory;
            _loadLevelsPanel = loadLevelsPanel;
            _gameDataSaveLoader = new GameDataSaveLoader();
            _content = content;
            _loadLevelsPanel.gameObject.SetActive(false);
            _levelLoader = levelLoader;
        }

        public void Activate()
        {
            if(_loadLevelsPanel.activeSelf == false)
            {
                _loadLevelsPanel.SetActive(true);
            }

            List<string> lvlNames = _gameDataSaveLoader.LoadWorldNamesJson();

            //CreateEditorState();

            if (lvlNames != null &&
                lvlNames.Count > 0)
            {
                CreateStatePanels(lvlNames);
            }
        }
        void CreateStatePanels(List<string> names)
        {
            foreach(string name in names)
            {
                GameStatePanel panel = _gameStatePanelFactory.Create(name, _content);
                panel.SetLevelLoader(_levelLoader);
                panel.MoneyText.text 
                    = _gameDataSaveLoader.LoadGameState(name).PlayerData.Money.ToString();
            }
        }
        public void CreateEditorState()
        {
            var panel = _gameStatePanelFactory.Create(GameConfiguration.SaveEditorGameStateName,
                _content);
            panel.SetLevelLoader(_levelLoader);
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
