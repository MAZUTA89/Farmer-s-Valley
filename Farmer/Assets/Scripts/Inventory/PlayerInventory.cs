using Scripts.FarmGameEvents;
using Scripts.Inventory;
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
        [SerializeField] private CanvasGroup _playerGroup;

        private void Awake()
        {
            if (s_instance == null)
            {
                s_instance = this;
            }
        }
        private void OnEnable()
        {
            GameEvents.OnTradePanelOpenClose += OnTradePanelAction;
        }
        private void OnDisable()
        {
            GameEvents.OnTradePanelOpenClose -= OnTradePanelAction;
        }

        public bool TryAddItem(InventoryItem inventoryItem)
        {
            if (_backPackInventory.IsFull() == false)
            {
                if (_backPackInventory.AddItem(inventoryItem))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (_activePackInventory.IsFull() == false)
            {
                if (_activePackInventory.AddItem(inventoryItem))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public bool TryPickupResource(ItemResource itemResource)
        {
            if (_backPackInventory.IsFull() == false)
            {
                _backPackInventory.AddItem(itemResource.InventoryItem);
                return true;
            }
            else if (_activePackInventory.IsFull() == false)
            {
                _activePackInventory.AddItem(itemResource.InventoryItem);
                return true;
            }
            return false;
        }
        public bool IsFull()
        {
            if (!_backPackInventory.IsFull() ||
                !_activePackInventory.IsFull())
            {
                return false;
            }
            else
                return true;
        }
        public static PlayerInventory Instance()
        {
            return s_instance;
        }

        public List<InventoryItem> GetAllItems()
        {
            List<InventoryItem> inventoryItems = new List<InventoryItem>();

            inventoryItems.AddRange(_activePackInventory.GetItems());
            inventoryItems.AddRange(_backPackInventory.GetItems());

            return inventoryItems;
        }

        public List<ItemContextData> GetAllItemsContextData()
        {
            List<ItemContextData> itemContextDatas = new List<ItemContextData>();

            itemContextDatas.AddRange(_activePackInventory.GetItemsContextData());
            itemContextDatas.AddRange(_backPackInventory.GetItemsContextData());

            return itemContextDatas;
        }

        public void RemoveItem(ItemContextData itemContextData)
        {
            if(itemContextData.ContextName == _activePackInventory.Container.name)
            {
                _activePackInventory.RemoveItem(itemContextData.Index);
            }
            if(itemContextData.ContextName == _backPackInventory.Container.name)
            {
                _backPackInventory.RemoveItem(itemContextData.Index);
            }
        }

        void OnTradePanelAction(bool isIgnore)
        {
            _playerGroup.blocksRaycasts = isIgnore;
        }

    }
}
