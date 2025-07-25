using System;
using System.Collections;
using System.Collections.Generic;
using HappyHarvest;
using UnityEngine;
using Zenject;

namespace HappyHarvest
{
    /// <summary>
    /// This allows to lock and unlock controlling the Player Controller. This is used by Animation Event to stop moving
    /// during e.g. a tool animation. This is added on the same object that have the Animation Controller so it can
    /// receive animation event.
    /// </summary>
    public class CharacterAnimationEventHandler : MonoBehaviour
    {
        private PlayerController m_Controller;
        private InputService _inputService;

        [Inject]
        public void Construct(InputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            //Get In Parent as this is on the GameObject with the animator and the controller is at the root of the
            //character.
            m_Controller = GetComponentInParent<PlayerController>();
        }

        void LockControl()
        {
            _inputService?.LockGamePlayControls();
            m_Controller?.ToggleControl(false);
        }

        void UnlockControl()
        {
            m_Controller?.ToggleControl(true);
            _inputService?.UnlockGamePlayControls();
        }
    }
}
