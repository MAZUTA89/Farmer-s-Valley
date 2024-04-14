using Scripts.InventoryCode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.ChestItem
{
    [RequireComponent(typeof(Animator))]
    
    public class Chest : MonoBehaviour
    {
        Animator _animator;
        int _aniIsCloseCode;
        int _aniIsOpenCode;
        /// <summary>
        /// True ���� ������, false ���� ������
        /// </summary>
        bool _openCloseFlag;
        List<InventoryItem> _inventoryItems;
        IInventoryPanelFactory _inventoryStoragePanelFactory;
        InventoryBase _chestStorage;

        [Inject]
        public void Construct(
            IInventoryPanelFactory inventoryStoragePanelFactory)
        {
            _inventoryStoragePanelFactory = inventoryStoragePanelFactory;
        }

        // Start is called before the first frame update
        void Start()
        {
            _animator = GetComponent<Animator>();
            _aniIsCloseCode = Animator.StringToHash("IsClose");
            _aniIsOpenCode = Animator.StringToHash("IsOpen");
            _openCloseFlag = true;
            _inventoryItems = new List<InventoryItem>();
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
    }
}