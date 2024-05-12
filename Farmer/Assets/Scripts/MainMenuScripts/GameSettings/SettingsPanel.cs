using Scripts.FarmGameEvents;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DropDownOption = TMPro.TMP_Dropdown.OptionData;

namespace Scripts.MainMenuCode
{
    public class SettingsPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropDownResolution;
        [SerializeField] private Toggle _toggleFullScreen;
        [SerializeField] private Slider _sliderMusic;
        [SerializeField] private Slider _sliderMain;
        [SerializeField] private Slider _sliderSFX;

        public TMP_Dropdown DropDownResolution => _dropDownResolution;
        public Toggle ToggleFullScreen => _toggleFullScreen;
        public Slider SliderMusic => _sliderMusic;
        public Slider SliderMain => _sliderMain;
        public Slider SliderSFX => _sliderSFX;

        List<Resolution> _availableResolutions;


        [Inject]
        public void Construct()
        {
        }
        public void InitializeDropDownResolution()
        {
            _dropDownResolution.options.Clear();

            _availableResolutions = new List<Resolution>();

            int currentResolutionIndex = 0;
            int i = 0;
            Resolution currentResolution = Screen.currentResolution;
            foreach (var resolution in Screen.resolutions)
            {
                _availableResolutions.Add(resolution);

                DropDownOption optionData = new();
                string resolutionText = $"{resolution.width}x{resolution.height}";
                optionData.text = resolutionText;
                _dropDownResolution.options.Add(optionData);

                if(currentResolution.width == resolution.width &&
                    currentResolution.height == resolution.height)
                {
                    currentResolutionIndex = i;
                }
                i++;
            }

            _dropDownResolution.value = currentResolutionIndex;

            TMP_Dropdown.DropdownEvent evnt = new TMP_Dropdown.DropdownEvent();
            evnt.AddListener((call) =>
            {
                var res = _availableResolutions[call];
                Screen.SetResolution(res.width, res.height, _toggleFullScreen.isOn);
            });
            _dropDownResolution.onValueChanged = evnt;

        }
        public void InitializeToggleFullScreen()
        {
            _toggleFullScreen.isOn = Screen.fullScreen;
            Toggle.ToggleEvent ev = new Toggle.ToggleEvent();
            ev.AddListener((call) =>
            {
                Screen.fullScreen = call;
            });
            _toggleFullScreen.onValueChanged = ev;
        }
    }
}
