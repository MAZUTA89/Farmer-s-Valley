using Scripts.SO.InventoryItem;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Scripts.InventoryCode
{
    public class InventoryCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] Image IconElement;
        [SerializeField] TextMeshProUGUI TextElement;
        Action _dragEvent;
        public InventoryItem InventoryItem { get; private set; }
        public  Transform DragParent { get; private set; }
        public Transform DragOrigin { get; private set; }
        public bool IsEmpty { get; private set; }

        public Image Icon => IconElement;
        public TextMeshProUGUI Text => TextElement;

        private void OnDisable()
        {
            _dragEvent = null;
        }
        public void InitializeDragParent(Transform dragParent)
        {
            DragOrigin = transform.parent;
            DragParent = dragParent;
        }
        public void OverwriteDragOrigin(Transform dragOrigin)
        {
            DragOrigin = dragOrigin;
        }
        public void InitializeItem(InventoryItem inventoryItem)
        {
            InventoryItem = inventoryItem;
            IsEmpty = false;
        }
        public void InitializeDragEvent(Action dragMethod)
        {
            _dragEvent += dragMethod;
        }
        public void SetEmpty(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetParent(DragParent);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
            PlaceInTheNearestCell();
            if (InventoryItem != null)
            {
                _dragEvent?.Invoke();
            }
        }

        void PlaceInTheNearestCell()
        {
            int closetIndex = 0;
            for (int i = 0; i < DragOrigin.transform.childCount; i++)
            {
                if (Vector3.Distance(DragOrigin.GetChild(i).position, transform.position) <
                    Vector3.Distance(DragOrigin.GetChild(closetIndex).position, transform.position))
                {
                    closetIndex = i;
                }
            }
            transform.SetParent(DragOrigin);
            transform.SetSiblingIndex(closetIndex);
        }

    }
}
