using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryCode
{
    public class PlayerInventory : MonoBehaviour
    {
        Inventory _inventory;
        [Inject]
        public void Construct(Inventory inventory)
        {
             _inventory = inventory;
        }

        private void Update()
        {
            if (_inventory != null)
            {
                _inventory.Render();
            }
        }
    }
}
