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
        Action _endDragEvent;
        Action<InventoryCell> _beginDragEvent;
        public InventoryItem InventoryItem { get; private set; }

        public Transform _globalVisualContext { get; private set; }
        public Transform OriginVisualContext { get; private set; }

        public Image Icon => IconElement;
        public TextMeshProUGUI Text => TextElement;

        public int BeginDragSiblingIndex { get; private set; }
        private void OnDisable()
        {
            _endDragEvent = null;
        }
        private void Start()
        {

        }
        private void Update()
        {
            InventoryItem?.RenderUI(this);
        }
        public void InitializeDragParent(Transform dragParent)
        {
            OriginVisualContext = transform.parent;
            _globalVisualContext = dragParent;
        }
        public void OverwriteDragOrigin(Transform dragOrigin)
        {
            OriginVisualContext = dragOrigin;

        }
        public void InitializeItem(InventoryItem inventoryItem)
        {
            InventoryItem = inventoryItem;
        }
        public void InitializeDragEvent(Action dragMethod)
        {
            _endDragEvent += dragMethod;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = OriginVisualContext.InverseTransformVector(Input.mousePosition);
            DragExtension.ShowNearestCellFor(this, OriginVisualContext);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragSiblingIndex = transform.GetSiblingIndex();
            Debug.Log($"Begin index: {BeginDragSiblingIndex}");
            _beginDragEvent?.Invoke(this);
            transform.SetParent(_globalVisualContext);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            InventoryBase inventory;
            if (DragExtension.CheckMouseIntersectionWithContainers(eventData,
                out inventory))
            {
                if (OriginVisualContext == inventory.Container)
                {
                    Debug.Log("Тот же контейнер");
                    DragExtension.PlaceInTheNearestCellLocal(OriginVisualContext,
                    this, BeginDragSiblingIndex);
                }
                else
                {
                    DragExtension.PlaceInTheNearestCellLocal(inventory.Container, this, BeginDragSiblingIndex);
                }
            }
            else
            {
                DragExtension.PlaceInTheNearestCellLocal(OriginVisualContext,
                    this, BeginDragSiblingIndex);
            }
            _endDragEvent?.Invoke();
        }
        public void Initialize(Transform globalVisualContext,
            InventoryItem inventoryItem, Action endDragEvent,
            Action<InventoryCell> beginDragEvent)
        {
            _globalVisualContext = globalVisualContext;
            OriginVisualContext = transform.parent;
            InventoryItem = inventoryItem;
            _endDragEvent = endDragEvent;
            _beginDragEvent = beginDragEvent;
        }
    }
}
