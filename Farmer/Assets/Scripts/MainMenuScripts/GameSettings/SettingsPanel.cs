using Scripts.FarmGameEvents;
using Scripts.InteractableObjects;
using Scripts.SaveLoader;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
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
        [SerializeField] private Transform _keyBoardBindingsContent;
        [SerializeField] private TextMeshProUGUI _interactableRebindText;
        [SerializeField] private CanvasGroup _settingsCanvasGroup;

        public TMP_Dropdown DropDownResolution => _dropDownResolution;
        public Toggle ToggleFullScreen => _toggleFullScreen;
        public Slider SliderMusic => _sliderMusic;
        public Slider SliderMain => _sliderMain;
        public Slider SliderSFX => _sliderSFX;
        public Transform KeyBoardBindingsContent => _keyBoardBindingsContent;
        public TextMeshProUGUI InteractableRebindText => _interactableRebindText;

        List<Resolution> _availableResolutions;

        InputService _inputService;

        KeyBindingPanelFactory _keyBindingPanelFactory;

        public InputActionAsset InputActionsAsset => _inputService.GetInputActionAsset();

        [Inject]
        public void Construct(FactoriesProvider factoriesProvider)
        {
            _keyBindingPanelFactory = (KeyBindingPanelFactory)factoriesProvider
                .GetFactory<KeyBindingPanelFactory>();
        }
        private void OnEnable()
        {
            GameEvents.OnPerformInteractiveRebindEvent += OnRebind;
        }
        private void OnDisable()
        {
            GameEvents.OnPerformInteractiveRebindEvent -= OnRebind;
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

                if (currentResolution.width == resolution.width &&
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
        public void InitializeBindingsKeys(InputService inputService)
        {
            InputActionAsset asset = inputService.GetInputActionAsset();

            foreach (var map in asset.actionMaps)
            {
                foreach (InputAction action in map.actions)
                {
                    
                    if(action.expectedControlType == "Vector2")
                    {
                        for (int i = 0; i < action.controls.Count; i++)
                        {
                            var control = action.controls[i];

                            var binding = action.GetBindingForControl(control);

                            KeyBindingPanel keyBindingPanel = _keyBindingPanelFactory
                                .Create(KeyBoardBindingsContent);

                            keyBindingPanel.Initialize(action, control);

                            keyBindingPanel.ActionNameText.text = binding.Value.name;
                            keyBindingPanel.KeyButtonText.text = control.displayName;
                        }
                    }
                    else
                    {
                        KeyBindingPanel keyBindingPanel = _keyBindingPanelFactory.Create(KeyBoardBindingsContent);

                        keyBindingPanel.ActionNameText.text = action.name;
                        keyBindingPanel.KeyButtonText.text = action.controls[0].displayName;

                        keyBindingPanel.Initialize(action, action.controls[0]);
                    }
                }
            }
        }
        void OnRebind(string text, bool activeSelf)
        {
            InteractableRebindText.text = text;
            _settingsCanvasGroup.blocksRaycasts = !activeSelf;
            InteractableRebindText.gameObject.transform.parent.
                transform.gameObject.SetActive(activeSelf);
        }
    }
}
