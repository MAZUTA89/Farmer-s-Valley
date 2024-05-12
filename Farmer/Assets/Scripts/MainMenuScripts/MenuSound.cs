using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.MainMenuScripts
{
    public class MenuSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _menuAudioSource;

        SettingsMenu _settingsMenu;

        [Inject]
        public void Construct(SettingsMenu settingsMenu)
        {
            _settingsMenu = settingsMenu;
        }
        private void OnEnable()
        {
            _settingsMenu.AddListenerAtMusicVolume(OnMusicValueChange);
        }
        private void OnDisable()
        {
            _settingsMenu.RemoveListenerAtMusicVolume(OnMusicValueChange);
        }
        private void Start()
        {
            
        }

        void OnMusicValueChange(float value)
        {
            _menuAudioSource.volume = value;
        }
    }
}
