using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : IDisposable
{
    const int c_firstItemCellIndex = 0;
    const int c_secondItemCellIndex = 1;
    const int c_thirdItemCellIndex = 2;
    const int c_fourthItemCellIndex = 3;
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
    public bool IsChosenCell(out int index)
    {
        index = 0;
        bool wasPerformed = false; 
        if(_inputActions.InventoryMap.ChooseFirstCell.WasPerformedThisFrame())
        {
            index = c_firstItemCellIndex;
            wasPerformed = true;
        }
        if (_inputActions.InventoryMap.ChooseSecondCell.WasPerformedThisFrame())
        {
            index = c_secondItemCellIndex;
            wasPerformed = true;
        }
        if (_inputActions.InventoryMap.ChooseThirdCell.WasPerformedThisFrame())
        {
            index = c_thirdItemCellIndex;
            wasPerformed = true;
        }
        if (_inputActions.InventoryMap.ChooseFourthCell.WasPerformedThisFrame())
        {
            index = c_fourthItemCellIndex;
            wasPerformed = true;
        }
        return wasPerformed;
    }

    public bool IsLBK()
    {
        return _inputActions.PlayerMap.LBK.WasPerformedThisFrame();
    }
    public bool IsRBK()
    {
        return _inputActions.PlayerMap.RBK.WasPerformedThisFrame();
    }
    public bool IsOpenCloseMenu()
    {
        return _inputActions.PlayerMap.OpenCloseGameMenu
            .WasPerformedThisFrame();
    }
    public void Dispose()
    {
        _inputActions.Disable();
        _inputActions.Dispose();
    }
}
