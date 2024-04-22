using Assets.Scripts.Placement;
using Scripts.InventoryCode;
using Scripts.PlacementCode;
using Scripts.SaveLoader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.InteractableObjects
{
    [RequireComponent(typeof(Animator))]
    
    public class Chest : PlacementItem, IOccupyingOneCell, ISaveLoadPlacementItem, IInteractable
    {
        Animator _animator;
        int _aniIsCloseCode;
        int _aniIsOpenCode;
        /// <summary>
        /// True если открыт, false если закрыт
        /// </summary>
        bool _openCloseFlag;
        List<IInventoryItem> _inventoryItems;
        IInventoryPanelFactory _inventoryStoragePanelFactory;
        InventoryBase _chestStorage;

        [Inject]
        public void ConstructChest(
            IInventoryPanelFactory inventoryStoragePanelFactory)
        {
            _inventoryStoragePanelFactory = inventoryStoragePanelFactory;
        }

        protected override void Start()
        {
            _animator = GetComponent<Animator>();
            _aniIsCloseCode = Animator.StringToHash("IsClose");
            _aniIsOpenCode = Animator.StringToHash("IsOpen");
            if(_inventoryItems == null)
            {
                _inventoryItems = new List<IInventoryItem>();
            }
            _chestStorage = _inventoryStoragePanelFactory.Create(_inventoryItems);
            _chestStorage.gameObject.SetActive(false);
            _openCloseFlag = true;

            base.Start();
        }

        void Update()
        {

        }

        private void OnMouseDown()
        {
            if (_openCloseFlag == true)
            {
                _animator.SetBool(_aniIsOpenCode, _openCloseFlag);
                _animator.SetBool(_aniIsCloseCode, false);
                _chestStorage.gameObject.SetActive(true);
            }
            else
            {
                _animator.SetBool(_aniIsCloseCode, true);
                _animator.SetBool(_aniIsOpenCode, _openCloseFlag);
                _inventoryItems = _chestStorage.GetItems();
                _chestStorage.gameObject.SetActive(false);
            }
            _openCloseFlag = !_openCloseFlag;
        }
        public void Initialize(List<IInventoryItem> inventoryItems)
        {
            _inventoryItems  = inventoryItems;
        }
        public Vector3Int GetOccupyingCell()
        {
            return PlacePosition;
        }

        public override PlacementItemData GetData()
        {
            ChestData chestData = new ChestData();
            var data = base.GetData();
            chestData.SetPosition(data.GetPosition());
            chestData.UpdateItems(_chestStorage.GetItems());
            return chestData;
        }
    }
}