using PimDeWitte.UnityMainThreadDispatcher;
using Scripts.MainMenuScripts;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using Zenject;

namespace Scripts.MainMenuCode
{
    public class NewGameMenu
    {
        GameDataSaveLoader _gameDataSaveLoader;
        NewGamePanel _newGamePanel;
        List<string> _lvlNames;
        LevelNameValidator _lvlNameValidator;
        public NewGameMenu([Inject(Id = "NewGamePanel")] NewGamePanel newGamePanel)
        {
            _gameDataSaveLoader = new GameDataSaveLoader();
            _newGamePanel = newGamePanel;
            _newGamePanel.gameObject.SetActive(false);
            _lvlNameValidator = new LevelNameValidator();
        }

        public void Activate()
        {
            if (_newGamePanel.gameObject.activeSelf == false)
            {
                _newGamePanel.gameObject.SetActive(true);
            }
            _lvlNames = _gameDataSaveLoader.LoadWorldNamesJson();
        }

        public async void OnCreate()
        {
            string input = _newGamePanel.InputText.text;
            _lvlNames = _gameDataSaveLoader.LoadWorldNamesJson();
            if (_lvlNames != null &&
                _lvlNames.Count > 0)
            {
                if (_lvlNameValidator.ValidateInputName(input) &&
                !_lvlNameValidator.CheckForExist(input, _lvlNames))
                {
                    StartLevel(input);
                    return;
                }
            }
            else if (_lvlNameValidator.ValidateInputName(input))
            {
                StartLevel(input);
            }
            else
            {
                _newGamePanel.InfoText.text = "Название не должно повторяться и " +
                    "начинаться с цифры!";
                _newGamePanel.InfoText.color = Color.red;
                await FadeText();
            }
        }
        async Task FadeText()
        {
            await Task.Run(() =>
            {
                UnityMainThreadDispatcher.Instance().Enqueue(async () =>
                {
                    Color startColor = _newGamePanel.InfoText.color;
                    Color targetColor = new Color(startColor.r, startColor.g, startColor.b,
                        0);
                    float fadeDuration = 10;
                    float elapsedTime = 0f;

                    while (elapsedTime < fadeDuration)
                    {
                        float t = elapsedTime / fadeDuration;
                        _newGamePanel.InfoText.color = Color.Lerp(startColor, targetColor, t);
                        elapsedTime += Time.deltaTime;
                        await Task.Yield();
                    }

                    _newGamePanel.InfoText.color = targetColor;
                });
            });
             
            
        }
        void StartLevel(string name)
        {
            
            GameDataState gameDataState = new(name);
            LoadedData.Instance().Initialize(gameDataState, true);
        }
        public void OnBack()
        {
            _newGamePanel.gameObject.SetActive(false);
        }
    }
}
