using Scripts.FarmGameEvents;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

        private InputActionRebindingExtensions.RebindingOperation _rebindOperation;


        public void UpdateUIActionParameters(string controlName)
        {
            KeyButtonText.text = controlName;
        }

        public void Initialize(InputAction inputAction, InputControl control)
        {
            _inputAction = inputAction;
            _inputControl = control;
        }
        void StartInteractiveBindingOperation(InputAction inputAction, int bindingIndex)
        {
            _rebindOperation?.Cancel();
            void CleanUp()
            {
                _rebindOperation?.Dispose();
                _rebindOperation = null;
            }

            inputAction.Disable();
            GameEvents.InvokeOnPerformInteractiveRebindEvent(c_textWhileRebind, true);

            _rebindOperation = inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnCancel((callback) =>
                {
                    CleanUp();
                })
                .OnComplete((callback) =>
                {
                    callback.Dispose();
                    CleanUp();
                    _inputAction.Enable(); 
                    string controlName;
                    if (_inputAction.bindings[bindingIndex].isPartOfComposite)
                    {
                        var binding = _inputAction.bindings[bindingIndex];

                        controlName = _inputControl.displayName;
                    }
                    else
                    {
                        controlName = _inputControl.displayName;
                    }
                    UpdateUIActionParameters(controlName);
                    GameEvents.InvokeOnPerformInteractiveRebindEvent(c_textWhileRebind, false);
                })
                .Start();
            

           

            //_rebindOperation.Start();
        }
        public void OnChangeControl()
        {
            StartInteractiveBindingOperation(_inputAction, _inputAction.GetBindingIndexForControl(_inputControl));
        }
    }
}
