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
        [SerializeField] private Image _displayImage;
        private void OnEnable()
        {
            OnTrueConditionEvent += OnTrueCondition;
            OnFalseConditionEvent += OnFalseCondition;
        }
        private void OnDisable()
        {
            OnTrueConditionEvent -= OnTrueCondition;
            OnFalseConditionEvent -= OnFalseCondition;
        }
        public static void InvokeTrueConditionEvent(IInventoryItem inventoryItem)
        {
            OnTrueConditionEvent?.Invoke(inventoryItem);
        }
        public static void InvokeFalseConditionEvent()
        {
            OnFalseConditionEvent?.Invoke();
        }
        public void OnTrueCondition(IInventoryItem inventoryItem)
        {
            _displayImage.gameObject.SetActive(true);
            _displayImage.sprite = inventoryItem.Icon;
        }
        public void OnFalseCondition()
        {
            _displayImage.gameObject.SetActive(false);
        }
        private void Update()
        {
            _displayImage.transform.position = Input.mousePosition;
        }
    }
}
