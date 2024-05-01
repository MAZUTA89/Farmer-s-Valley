using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ItemUsage
{
    internal class UsableItemKursorHandler : MonoBehaviour
    {
        public static event Action<IInventoryItem> OnTrueConditionEvent;
        public static event Action OnFalseConditionEvent;
        public static event Action<Vector3> OnMovePlateEvent;
        public static event Action OnDeactivatePlateEvent;
        [SerializeField] private Image _displayImage;
        [SerializeField] private SpriteRenderer _plate;
        [ColorUsage(true)]
        [SerializeField] private Color _trueColor;
        [ColorUsage(true)]
        [SerializeField] private Color _falseColor;

        
        private void OnEnable()
        {
            OnTrueConditionEvent += OnTrueCondition;
            OnFalseConditionEvent += OnFalseCondition;
            OnDeactivatePlateEvent += OnDeactivatePlate;
            OnMovePlateEvent += OnMovePlate;
        }
        private void OnDisable()
        {
            OnTrueConditionEvent -= OnTrueCondition;
            OnFalseConditionEvent -= OnFalseCondition;
            OnDeactivatePlateEvent -= OnDeactivatePlate;
            OnMovePlateEvent -= OnMovePlate;
        }
        public static void InvokeTrueConditionEvent(IInventoryItem inventoryItem)
        {
            OnTrueConditionEvent?.Invoke(inventoryItem);
        }
        public static void InvokeFalseConditionEvent()
        {
            OnFalseConditionEvent?.Invoke();
        }
        public static void InvokeDeactivatePlateEvent()
        {
            Debug.LogError("Deactiivate plate!");
            OnDeactivatePlateEvent?.Invoke();
        }
        public static void InvokeMovePlateEvent(Vector3 position)
        {
            OnMovePlateEvent?.Invoke(position);
        }
        public void OnTrueCondition(IInventoryItem inventoryItem)
        {
            _plate.gameObject.SetActive(true);
            _displayImage.gameObject.SetActive(true);
            _displayImage.sprite = inventoryItem.Icon;
            _plate.color = _trueColor;
        }
        public void OnFalseCondition()
        {
            _displayImage.gameObject.SetActive(false);
            _plate.color = _falseColor;
        }
        public void OnDeactivatePlate()
        {
            _plate.gameObject.SetActive(false);
        }
        public void OnMovePlate(Vector3 position)
        {
            _plate.gameObject.transform.position = position;
        }
        
        private void Update()
        {
            _displayImage.transform.position = Input.mousePosition;
            if(InventoryStorage.IsMouseStay)
            {
                _plate.gameObject.SetActive(false);
            }
            
        }
    }
}
