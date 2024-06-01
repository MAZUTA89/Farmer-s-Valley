using Assets.Scripts.LevelsLoading;
using PimDeWitte.UnityMainThreadDispatcher;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class LevelLoader
    {
        AsyncOperation _loadTask;
        LoadScreenPanel _loadScreenPanel;
        public LevelLoader(LoadScreenPanel loadScreenPanel)
        {
            _loadScreenPanel = loadScreenPanel;
            _loadScreenPanel.Deactivate();
        }
        public async Task LoadLevel(string lvlName)
        {
            await Task.Run(() =>
            {
                UnityMainThreadDispatcher.Instance().Enqueue(async () =>
                {
                    _loadScreenPanel.Activate();

                    _loadTask = SceneManager.LoadSceneAsync(lvlName);

                    _loadTask.completed += (operation) =>
                    {
                        Debug.Log("Сцена загружена");
                    };

                    Debug.Log("Начало загрузки сцены");

                    while (!_loadTask.isDone)
                    {
                        Debug.Log("Загрузка: " + _loadTask.progress);
                        await Task.Yield();
                        _loadScreenPanel.ProgressSlider.value = Mathf.Clamp01(_loadTask.progress / 0.9f);
                    }
                });
            });

        }


    }
}
