using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scripts.MainMenuScripts
{
    public class BindingPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _mapNameNext;
        public TextMeshProUGUI MapNameText => _mapNameNext;
    }
}
