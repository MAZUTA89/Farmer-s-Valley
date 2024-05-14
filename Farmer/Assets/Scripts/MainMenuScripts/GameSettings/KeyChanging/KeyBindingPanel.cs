using Scripts.FarmGameEvents;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace Scripts.MainMenuCode
{
    public class KeyBindingPanel : MonoBehaviour
    {
        const string c_textWhileRebind = "Waiting for input ...";
        [SerializeField] private TextMeshProUGUI _actionNameText;
        [SerializeField] private Button _keyButton;
        [SerializeField] private TextMeshProUGUI _keyButtonText;

        public TextMeshProUGUI ActionNameText => _actionNameText;
        public TextMeshProUGUI KeyButtonText => _keyButtonText;
        public Button KeyButton => _keyButton;

        InputAction _inputAction;
        InputControl _inputControl;
        int _bindingIndex;
        bool _enableValueWhenInitialized;

        private InputActionRebindingExtensions.RebindingOperation _rebindOperation;


        public void UpdateUIActionParameters(string controlName)
        {
            KeyButtonText.text = controlName;
        }

        public void Initialize(InputAction inputAction, InputControl control)
        {
            _enableValueWhenInitialized = inputAction.enabled;
            _bindingIndex = inputAction.GetBindingIndexForControl(control);
            _inputAction = inputAction;
            _inputControl = control;
        }
        void StartInteractiveBindingOperation(InputAction inputAction, int bindingIndex)
        {
            if (bindingIndex == -1)
                return;
            _keyButton.enabled = false;
            _rebindOperation?.Cancel();
            void CleanUp()
            {
                _rebindOperation?.Dispose();
                _rebindOperation = null;
                _keyButton.enabled = true;
            }
            
            inputAction.Disable();
            GameEvents.InvokeOnPerformInteractiveRebindEvent(c_textWhileRebind, true);

            _rebindOperation = inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnCancel((callback) =>
                {
                    GameEvents.InvokeOnPerformInteractiveRebindEvent(c_textWhileRebind, false);
                    CleanUp();
                })
                //.WithCancelingThrough("<Keyboard>/space")
                .OnComplete((callback) =>
                {
                    CleanUp();
                    if(_enableValueWhenInitialized == true)
                    {
                        inputAction.Enable();
                    }
                    string controlName;
                    var binding = _inputAction.bindings[bindingIndex];
                    controlName = binding.ToDisplayString();
                    UpdateUIActionParameters(controlName);

                    GameEvents.InvokeOnPerformInteractiveRebindEvent(c_textWhileRebind, false);
                });

            _rebindOperation.Start();
        }
        
        public void OnChangeControl()
        {
            StartInteractiveBindingOperation(_inputAction, _bindingIndex);
        }
    }
}
