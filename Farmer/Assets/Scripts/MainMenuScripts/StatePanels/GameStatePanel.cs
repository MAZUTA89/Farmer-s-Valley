using Assets.Scripts;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.MainMenuCode
{
    public class GameStatePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI NameTextField;
        public TextMeshProUGUI NameText => NameTextField;

        
        public void Load()
        {
            SceneManager.LoadScene(GameConfiguration.FarmSceneName);
        }
    }
}
