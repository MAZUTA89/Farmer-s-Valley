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

        private InputActionRebindingExtensions.RebindingOperation _rebindOperation;


        public void UpdateUIActionParameters(string controlName)
        {
            KeyButtonText.text = controlName;
        }

        public void Initialize(InputAction inputAction, InputControl control)
        {
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
                //Debug.Log("OPERATION CLEAN UP!");
            }
            
            inputAction.Disable();
            Debug.Log("OPEN WHILE REBIND WINDOW!");
            GameEvents.InvokeOnPerformInteractiveRebindEvent(c_textWhileRebind, true);

            _rebindOperation = inputAction.PerformInteractiveRebinding(bindingIndex)
                .OnCancel((callback) =>
                {
                    Debug.Log("OPERATION CANCELD");
                    GameEvents.InvokeOnPerformInteractiveRebindEvent(c_textWhileRebind, false);
                    CleanUp();
                })
                .WithCancelingThrough("<Keyboard>/space")
                .OnApplyBinding((op, path) =>
                {
                    CleanUp();
                    //_inputAction.Enable(); 
                    _inputAction.ChangeBinding(bindingIndex)
                    .WithPath(path);
                    string controlName;
                    var binding = _inputAction.bindings[bindingIndex];
                    controlName = binding.ToDisplayString();
                    UpdateUIActionParameters(controlName);
                    Debug.Log("CLOSE WHILE REBIND WINDOW!");
                   
                    Debug.Log("OPERATION COMPLETED!!!");
                    GameEvents.InvokeOnPerformInteractiveRebindEvent(c_textWhileRebind, false);
                    Debug.Log("APPLY BINDING! - " + path);
                });
                //.OnComplete((callback) =>
                //{
                    
                //});

            _rebindOperation.Start();
        }
        //public bool ResolveActionAndBinding(out InputAction action, out int bindingIndex)
        //{
        //    bindingIndex = -1;

        //    action = m_Action?.action;
        //    if (action == null)
        //        return false;

        //    if (string.IsNullOrEmpty(m_BindingId))
        //        return false;

        //    // Look up binding index.
        //    var bindingId = new Guid(m_BindingId);
        //    bindingIndex = action.bindings.IndexOf(x => x.id == bindingId);
        //    if (bindingIndex == -1)
        //    {
        //        Debug.LogError($"Cannot find binding with ID '{bindingId}' on '{action}'", this);
        //        return false;
        //    }

        //    return true;
        //}
        public void OnChangeControl()
        {
            StartInteractiveBindingOperation(_inputAction, _bindingIndex);
        }
    }
}
