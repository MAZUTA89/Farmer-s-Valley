using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : IDisposable
{
    InputActions _inputActions;

    public InputService()
    {
        _inputActions = new InputActions();
        _inputActions.Enable();
    }
    
    public Vector2 GetMovement()
    {
        return _inputActions.PlayerMap.Movement.ReadValue<Vector2>();
    }

    public void Dispose()
    {
        _inputActions.Disable();
        _inputActions.Dispose();
    }
}
