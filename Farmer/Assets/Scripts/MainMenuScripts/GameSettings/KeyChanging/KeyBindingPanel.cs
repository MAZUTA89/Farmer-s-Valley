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
        [SerializeField] private TextMeshProUGUI _actionNameText;
        [SerializeField] private Button _keyButton;
        [SerializeField] private TextMeshProUGUI _keyButtonText;

        public TextMeshProUGUI ActionNameText => _actionNameText;
        public TextMeshProUGUI KeyButtonText => _keyButtonText;
        public Button KeyButton => _keyButton;

        InputAction _inputAction;

        public void InitializeAction(InputAction inputAction)
        {
            _inputAction = inputAction;
            UpdateUIActionParameters();
        }

        public void UpdateUIActionParameters()
        {
            ActionNameText.text = _inputAction.name;
            KeyButtonText.text = _inputAction.controls[0].displayName;
        }
    }
}
