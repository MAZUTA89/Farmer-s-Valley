using Scripts.InventoryCode;
using Scripts.PlayerCode;
using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using Scripts.Inventory;

namespace Scripts.SellBuy
{
    public class TradeService
    {
        PlayerInventory _playerInventory;
        PlayerMoney _playerMoney;
        [Inject]
        public void Construct(PlayerInventory playerInventory,
            PlayerMoney playerMoney)
        {
            _playerInventory = playerInventory;
            _playerMoney = playerMoney;
        }
        public bool BuyCondition(InventoryItem item)
        {
            if(_playerInventory.IsFull() && item.BuyPrice > 0)
            {
                return false;
            }
            if(item.Consumable)
            {
                return item.BuyPrice <= _playerMoney.Money;
            }
            else
            {
                return false;
            }

        }
       
        public void Buy(InventoryItem item, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                InventoryItem inventoryItem =
                    (InventoryItem)item.Clone();
                _playerInventory.TryAddItem(inventoryItem);
            }
            _playerMoney.Money -= item.BuyPrice;
        }
        public bool SellCondition(InventoryItem item)
        {
            return item is ProductItem && item.BuyPrice > 0;

        }
        public void Sell(ItemContextData contextData)
        {
            var item = contextData.Item;
            Debug.Log("Удаляю :" + item.DisplayName);
            _playerInventory.RemoveItem(contextData);

            _playerMoney.Money += item.BuyPrice * item.Count;
        }
        public int ItemAmount(InventoryItem item)
        {
            return Mathf.RoundToInt(_playerMoney.Money / item.BuyPrice);
        }

    }
}
