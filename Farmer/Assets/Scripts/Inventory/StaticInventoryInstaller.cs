using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    public class StaticInventoryInstaller : MonoBehaviour
    {
        [Header("Back Pack")]
        [SerializeField] List<InventoryItem> _backPackStartItemKit;
        [SerializeField] InventoryBase _backPackInventory;
        [Space]
        [Space]
        [Header("Active Pack")]
        [SerializeField] List<InventoryItem> _activeStartItemKit;
        [SerializeField] InventoryBase _activeInventory;
        [Space]
        [Space]
        [Header("Seller Pack")]
        [SerializeField] List<InventoryItem> _sellerStartItemKit;
        [SerializeField] InventoryBase _sellerInventory;

        private void Start()
        {
            List<IInventoryItem> activeItems = new List<IInventoryItem>(_activeStartItemKit);
            List<IInventoryItem> backItems = new List<IInventoryItem>(_backPackStartItemKit);
            _backPackInventory.Initialize(backItems);
            _activeInventory.Initialize(activeItems);
            //_sellerInventory.Initialize(_sellerStartItemKit);
        }
    }
}
