using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

namespace Scripts.NextLevel
{
    public class PreviousLevel : MonoBehaviour
    {
        public void OnAction()
        {
            SceneManager.LoadScene(GameConfiguration.FarmSceneName);
        }
    }
}
