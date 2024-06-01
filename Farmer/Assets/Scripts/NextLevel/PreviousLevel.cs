using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;
using Zenject;

namespace Scripts.NextLevel
{
    public class PreviousLevel : MonoBehaviour
    {
        LevelLoader _levelLoader;
        [Inject]
        public void Construct(LevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }

        public async void OnAction()
        {
           await _levelLoader.LoadLevel(GameConfiguration.FarmSceneName);
        }
    }
}
