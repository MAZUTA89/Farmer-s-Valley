using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Scripts.InventoryCode
{
    public class ChestInventory : InventoryStorage
    {
        private RectTransform rectTransform;
        private Canvas _canvas;
        protected override void Start()
        {
            base.Start();
            rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
        }
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform,
            Input.mousePosition,
                _canvas.worldCamera,
                out localPoint
            );

            rectTransform.localPosition = localPoint;
        }
        public override void OnExitTheGame()
        {
        }
        public override void Update()
        {
        }
    }
}
