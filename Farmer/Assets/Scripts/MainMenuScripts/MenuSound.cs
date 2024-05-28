using Scripts.MainMenuCode;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Scripts.MainMenuScripts
{
    public class MenuSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _menuAudioSource;
        [SerializeField] private AudioMixer _mixer;

        SettingsMenu _settingsMenu;

        [Inject]
        public void Construct(SettingsMenu settingsMenu)
        {
            _settingsMenu = settingsMenu;
        }
        private void OnEnable()
        {
            _settingsMenu.AddListenerAtMusicVolume(OnMusicValueChange);
            _settingsMenu.AddListenerAtMainVolume(OnMainValueChange);
            _settingsMenu.AddListenerAtSFXVolume(OnSFXValueChange);
        }
        private void OnDisable()
        {
            _settingsMenu.RemoveListenerAtMusicVolume(OnMusicValueChange);
            _settingsMenu.RemoveListenerAtMainVolume(OnMainValueChange);
            _settingsMenu.RemoveListenerAtSFXVolume(OnSFXValueChange);
        }

        void OnMusicValueChange(float value)
        {
            _mixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Max(0.0001f, value)) * 30f);

        }
        void OnMainValueChange(float value)
        {
            _mixer.SetFloat("MainVolume", Mathf.Log10(Mathf.Max(0.0001f, value)) * 30f);
        }

        void OnSFXValueChange(float value)
        {
            _mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(0.0001f, value)) * 30f);
        }

    }
}
