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
        static PlayerInventory s_instance;
        [SerializeField] InventoryBase _activePackInventory;
        [SerializeField] InventoryBase _backPackInventory;

        private void Awake()
        {
            if(s_instance == null)
            {
                s_instance = this;
            }
        }

        public bool TryAddItem(InventoryItem inventoryItem)
        {
            if (_backPackInventory.IsFull() == false)
            {
                _backPackInventory.AddItem(inventoryItem);
                return true;
            }
            else if (_activePackInventory.IsFull() == false)
            {
                _activePackInventory.AddItem(inventoryItem);
                return true;
            }
            return false;
        }
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
        public static PlayerInventory Instance()
        {
            return s_instance;
        }

    }
}
