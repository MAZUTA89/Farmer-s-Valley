using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts;

namespace Scripts.NextLevel
{
    public class NextLevel : MonoBehaviour
    {
        public void OnView()
        {
            SceneManager.LoadScene(GameConfiguration.NextLevelName);
        }
    }
}
