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

        [Inject]
        public void Construct(SettingsMenu settingsMenu,
            GameDataState gameDataState)
        {
            _settingsMenu = settingsMenu;
            _gameDataState = gameDataState;
        }

        public void OnView()
        {
            GameEvents.InvokeExitTheGameEvent();
            LoadedData.Instance().InitializeGameStateData(_gameDataState, false);
            _settingsMenu.Save();
            SceneManager.LoadScene(GameConfiguration.NextLevelName);
        }
    }
}
