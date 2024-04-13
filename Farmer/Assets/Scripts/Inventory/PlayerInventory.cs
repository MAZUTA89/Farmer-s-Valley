using Scripts.InventoryCode.ItemResources;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryCode
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] InventoryBase _activePackInventory;
        [SerializeField] InventoryBase _backPackInventory;
        
        
        public bool TryPickupResource(ItemResource itemResource)
        {
            if(_backPackInventory.IsFull() == false)
            {
                _backPackInventory.AddItem(itemResource.InventoryItem);
                return true;
            }
            else if(_activePackInventory.IsFull() == false)
            {
                _activePackInventory.AddItem(itemResource.InventoryItem);
                return true;
            }
            return false;
        }

    }
}
