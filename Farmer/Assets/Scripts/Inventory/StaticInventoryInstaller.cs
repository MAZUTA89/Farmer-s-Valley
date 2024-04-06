using Scripts.SO.InventoryItem;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.InventoryCode
{
    public class StaticInventoryInstaller : MonoBehaviour
    {
        [Header("Back Pack")]
        [SerializeField] List<InventoryItemAssetData> _backPackStartItemKit;
        [SerializeField] InventoryBase _backPackInventory;
        [Space]
        [Space]
        [Header("Active Pack")]
        [SerializeField] List<InventoryItemAssetData> _activeStartItemKit;
        [SerializeField] InventoryBase _activeInventory;
        [Space]
        [Space]
        [Header("Seller Pack")]
        [SerializeField] List<InventoryItemAssetData> _sellerStartItemKit;
        [SerializeField] InventoryBase _sellerInventory;

        private void Start()
        {
            _backPackInventory.Initialize(_backPackStartItemKit);
            _activeInventory.Initialize(_activeStartItemKit);
            //_sellerInventory.Initialize(_sellerStartItemKit);
        }
    }
}
