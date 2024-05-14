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
        InputActionAsset inputActionsAsset = _inputActions.asset;
        //foreach (InputActionMap map in inputActionsAsset.actionMaps)
        //{
        //    foreach (InputAction action in map.actions)
        //    {
        //        if (action.name == "LBK")
        //        {
        //            var control = action.controls[0];
        //            int bindIndex = action.GetBindingIndexForControl(control);
        //            action.Disable();
        //            var operation = action.PerformInteractiveRebinding()
        //                .WithControlsExcluding("Mouse")
        //                .OnComplete((callback) =>
        //                {
        //                    callback.Dispose();
        //                    action.Enable();
        //                    Debug.Log("New lbk control: " + action.controls[0]);
        //                    string json = inputActionsAsset.SaveBindingOverridesAsJson();
        //                })
        //                .Start();

        //            action.ChangeBinding(bindIndex).WithPath("<Keyboard>/space");
        //            InputBinding? binding = action.GetBindingForControl(control);
        //            (int, string) k = ((bindIndex, "<Keyboard>/space"));
        //            action.ApplyBindingOverride(k.Item1, k.Item2);
        //            string json = inputActionsAsset.SaveBindingOverridesAsJson();
        //            inputActionsAsset.LoadBindingOverridesFromJson(json);
        //            inputActionsAsset.RemoveAllBindingOverrides();
        //            action.RemoveAllBindingOverrides();
        //            json = inputActionsAsset.SaveBindingOverridesAsJson();
        //        }
        //        //Debug.Log("Name: " + action.name);
        //        ////Debug.Log("BindingIndex: " + action.GetBindingIndex().ToString());
        //        //foreach (var control in action.controls)
        //        //{
        //        //    Debug.Log("Control :" + control.displayName);
        //        //    int index = action.GetBindingIndexForControl(control);
        //        //    action.ChangeBinding(index).WithPath(;
        //        //    Debug.Log("Control binding index: " + (action.GetBindingIndexForControl(control)));
        //        //}
        //        //foreach (var binding in action.bindings)
        //        //{
        //        //    Debug.Log("- binding name: " + binding.name);
        //        //}
        //        //Debug.Log("bindingDisplay: " + action.GetBindingDisplayString());
        //    }
        //}
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
        return _inputActions.MenuActions.OpenCloseGameMenu
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
