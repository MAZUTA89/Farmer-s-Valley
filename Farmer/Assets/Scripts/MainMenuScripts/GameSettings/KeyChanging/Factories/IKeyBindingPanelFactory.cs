using System;
using System.Collections.Generic;
using Scripts.InteractableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.MainMenuCode
{
    public interface IKeyBindingPanelFactory : IGameObjectFactory<KeyBindingPanel, Transform>
    {

    }
}
