using Scripts.MainMenuScripts;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.MainMenuCode
{
    public class NewGameMenu
    {
        
        GameDataSaveLoader _gameDataSaveLoader;
        NewGamePanel _newGamePanel;
        List<string> _lvlNames;
        LevelNameValidator _lvlNameValidator;
        public NewGameMenu([Inject(Id = "NewGamePanel")]NewGamePanel newGamePanel)
        {
            _gameDataSaveLoader = new GameDataSaveLoader();
            _newGamePanel = newGamePanel;
            _newGamePanel.gameObject.SetActive(false);
            _lvlNameValidator = new LevelNameValidator();
        }

        public void Activate()
        {
            if(_newGamePanel.gameObject.activeSelf == false)
            {
                _newGamePanel.gameObject.SetActive(true);
            }
            _lvlNames = _gameDataSaveLoader.LoadWorldNamesJson();
        }

        public void OnCreate()
        {
            string input = _newGamePanel.InputText.text;
            _lvlNames = _gameDataSaveLoader.LoadWorldNamesJson();
            if(_lvlNames != null &&
                _lvlNames.Count > 0)
            {
                if (_lvlNameValidator.ValidateInputName(input) &&
                _lvlNameValidator.CheckForExist(input, _lvlNames))
                {
                    
                }
            }
            
        }
        public void OnBack()
        {
            _newGamePanel.gameObject.SetActive(false);
        }
    }
}
