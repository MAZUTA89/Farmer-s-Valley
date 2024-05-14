using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Scripts.MainMenuCode
{
    public class KeyBindingPanelFactory : IKeyBindingPanelFactory
    {
        KeyBindingPanel _keyBindingPanelTemplate;
        IInstantiator _instantiator;
        public KeyBindingPanelFactory(KeyBindingPanel keyBindingPanelTemplate, 
            IInstantiator instantiator)
        {
             _keyBindingPanelTemplate = keyBindingPanelTemplate;
            _instantiator = instantiator;
        }
        public KeyBindingPanel Create(Transform createData)
        {
            var panel = _instantiator.InstantiatePrefabForComponent<KeyBindingPanel>(_keyBindingPanelTemplate, createData);

            return panel;
        }
    }
}
