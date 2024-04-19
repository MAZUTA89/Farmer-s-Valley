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
    
    public class Chest : PlacementItem, IOccupyingOneCell, ISaveLoadItem, IInteractable
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

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _animator = GetComponent<Animator>();
            _aniIsCloseCode = Animator.StringToHash("IsClose");
            _aniIsOpenCode = Animator.StringToHash("IsOpen");
            _openCloseFlag = true;
            _inventoryItems = new List<IInventoryItem>();
            _chestStorage = _inventoryStoragePanelFactory.Create(_inventoryItems);
            _chestStorage.gameObject.SetActive(false);
        }

        // Update is called once per frame
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

        public Vector2Int GetOccupyingCell()
        {
            return PlacePosition;
        }
        

        IItemData ISaveLoadItem.GetData()
        {
            ChestData itemData = new ChestData();
            itemData.SetPosition(PlacePosition);
            itemData.UpdateItems(_inventoryItems);
            return itemData;
        }
    }
}