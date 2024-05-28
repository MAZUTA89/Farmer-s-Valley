using System;
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
        InputActionAsset inputActionsAsset = _inputActions.asset;
    }
    
    public Vector2 GetMovement()
    {
        return _inputActions.PlayerMap.Movement.ReadValue<Vector2>();
    }
    public bool IsChosenCell(out int index)
    {
        index = 0;
        bool wasPerformed = false; 
        if(_inputActions.InventoryMap.Choosefirstcell.WasPerformedThisFrame())
        {
            index = c_firstItemCellIndex;
            wasPerformed = true;
        }
        if (_inputActions.InventoryMap.Choosesecondcell.WasPerformedThisFrame())
        {
            index = c_secondItemCellIndex;
            wasPerformed = true;
        }
        if (_inputActions.InventoryMap.Choosethirdcell.WasPerformedThisFrame())
        {
            index = c_thirdItemCellIndex;
            wasPerformed = true;
        }
        if (_inputActions.InventoryMap.Choosefourthcell.WasPerformedThisFrame())
        {
            index = c_fourthItemCellIndex;
            wasPerformed = true;
        }
        return wasPerformed;
    }

    public bool IsLBK()
    {
        return _inputActions.PlayerMap.Interact.WasPerformedThisFrame();
    }
    
    public bool IsOpenCloseMenu()
    {
        return _inputActions.MenuActions.OpenClosegamemenu
            .WasPerformedThisFrame();
    }
    public void LockGamePlayControls()
    {
        _inputActions.PlayerMap.Disable();
    }
    public void UnlockGamePlayControls()
    {
        _inputActions.PlayerMap.Enable();
    }
    public void LockMenuControls()
    {
        _inputActions.MenuActions.Disable();
    }
    public void UnlockMenuControls()
    {
        _inputActions.MenuActions.Enable();
    }
    public bool IsOpenCloseBackPack()
    {
        return _inputActions.InventoryMap.OpenClosebackpack.WasPerformedThisFrame();
    }
    public void Dispose()
    {
        _inputActions.Disable();
        _inputActions.Dispose();
    }
    public InputActionAsset GetInputActionAsset()
    {
        return _inputActions.asset;
    }
}
