using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using Scripts.MainMenuCode;
using Scripts.SaveLoader;
using Zenject;
using Scripts.FarmGameEvents;

namespace Scripts.NextLevel
{
    public class NextLevel : MonoBehaviour
    {
        SettingsMenu _settingsMenu;
        GameDataState _gameDataState;
        LevelLoader _levelLoader;

        [Inject]
        public void Construct(SettingsMenu settingsMenu,
            GameDataState gameDataState,
            LevelLoader levelLoader)
        {
            _settingsMenu = settingsMenu;
            _gameDataState = gameDataState;
            _levelLoader = levelLoader;
        }

        public async void OnView()
        {
            GameEvents.InvokeExitTheGameEvent();
            LoadedData.Instance().InitializeGameStateData(_gameDataState, false);
            _settingsMenu.Save();
            await _levelLoader.LoadLevel(GameConfiguration.NextLevelName);
        }
    }
}
