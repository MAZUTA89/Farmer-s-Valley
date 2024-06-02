using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;


namespace Scripts.MainMenuCode
{
    public class SettingsMenu
    {
        SettingsPanel _settingsPanel;
        SettingsDataSaveLoader _settingSaveLoader;
        InputService _inputService;
        public bool Controls;

        public SettingsMenu(SettingsPanel settingsPanel,
            InputService inputService,
            bool initializeControls)
        {
            if (initializeControls)
            {
                _inputService = inputService;
                _settingsPanel = settingsPanel;
                _settingSaveLoader = new SettingsDataSaveLoader();
                _settingsPanel.gameObject.SetActive(false);
                _settingsPanel.InitializeDropDownResolution();
                _settingsPanel.InitializeToggleFullScreen();
            }
            Controls = initializeControls;

        }

        public void AddListenerAtMainVolume(UnityAction<float> action)
        {
            _settingsPanel.SliderMain.onValueChanged.AddListener(action);
        }
        public void AddListenerAtMusicVolume(UnityAction<float> action)
        {
            _settingsPanel.SliderMusic.onValueChanged.AddListener(action);
        }
        public void AddListenerAtSFXVolume(UnityAction<float> action)
        {
            _settingsPanel.SliderSFX.onValueChanged.AddListener(action);
        }
        public void RemoveListenerAtMainVolume(UnityAction<float> action)
        {
            _settingsPanel.SliderMain.onValueChanged.RemoveListener(action);
        }
        public void RemoveListenerAtMusicVolume(UnityAction<float> action)
        {
            _settingsPanel.SliderMusic.onValueChanged.RemoveListener(action);
        }
        public void RemoveListenerAtSFXVolume(UnityAction<float> action)
        {
            _settingsPanel.SliderSFX.onValueChanged.RemoveListener(action);
        }
        public void InitializeKeyBindings()
        {
            _settingsPanel.InitializeBindingsKeys(_inputService);
        }
        public void Activate()
        {
            if (_settingsPanel.gameObject.activeSelf == false)
            {
                _settingsPanel.gameObject.SetActive(true);
            }
        }
        public void Back()
        {
            if (_settingsPanel.gameObject.activeSelf)
            {
                _settingsPanel.gameObject.SetActive(false);
            }
        }

        public void Load(SettingsData settingsData)
        {
            Screen.SetResolution(settingsData.ResolutionWidth, settingsData.ResolutionHeight, settingsData.IsFoolScreen);

            _settingsPanel.SliderMain.value = settingsData.MainVolume;
            _settingsPanel.SliderMusic.value = settingsData.MusicVolume;
            _settingsPanel.SliderSFX.value = settingsData.SFXVolume;

            var asset = _inputService.GetInputActionAsset();

            asset.LoadBindingOverridesFromJson(settingsData.InputOverrides);
        }
        public void Save()
        {
            SettingsData settingsData = new SettingsData();

            settingsData.IsFoolScreen = Screen.fullScreen;
            settingsData.SetResolution(Screen.currentResolution);
            settingsData.MainVolume = _settingsPanel.SliderMain.value;
            settingsData.MusicVolume = _settingsPanel.SliderMusic.value;
            settingsData.SFXVolume = _settingsPanel.SliderSFX.value;
            settingsData.InputOverrides = _inputService.GetInputActionAsset().SaveBindingOverridesAsJson();

            _settingSaveLoader.Save(settingsData);
            if (LoadedData.Instance() != null)
            {
                LoadedData.Instance().InitializeSettingsData(settingsData, false);
            }
        }
    }
}
