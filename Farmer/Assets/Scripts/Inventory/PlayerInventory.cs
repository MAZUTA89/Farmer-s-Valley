using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.InventoryCode
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] Inventory _backPackInventory;
        [SerializeField] Inventory _activeInventory;
        [Inject]
        public void Construct(Inventory backPackInventory)
        {
             _backPackInventory = backPackInventory;
        }

        private void Update()
        {
           
        }
    }
}
