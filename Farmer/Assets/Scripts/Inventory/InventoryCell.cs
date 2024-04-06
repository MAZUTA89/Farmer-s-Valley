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
        int _siblingIndex;
        public InventoryItem InventoryItem { get; private set; }

        public Transform _globalVisualContext { get; private set; }
        public Transform _originVisualContext { get; private set; }

        public Image Icon => IconElement;
        public TextMeshProUGUI Text => TextElement;

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
            _originVisualContext = transform.parent;
            _globalVisualContext = dragParent;
        }
        public void OverwriteDragOrigin(Transform dragOrigin)
        {
            _originVisualContext = dragOrigin;

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
            transform.position = _originVisualContext.InverseTransformVector(Input.mousePosition);
            EndDragExtension.ShowNearestCellFor(this, _originVisualContext);
        }
        int currentIndex;
        public void OnBeginDrag(PointerEventData eventData)
        {
            currentIndex = transform.GetSiblingIndex();
            Debug.Log($"Begin index: {currentIndex}");
            transform.SetParent(_globalVisualContext);
            _beginDragEvent?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //InventoryBase inventory;
            //if (EndDragExtension.CheckMouseIntersectionWithContainers(eventData,
            //    out inventory))
            //{
            //    if(_originVisualContext == inventory.Container)
            //    {
            //        Debug.Log("Тот же контейнер");
            //        EndDragExtension.PlaceInTheNearestCell(_originVisualContext,
            //        this);
            //    }
            //    else
            //    {
            //        EndDragExtension.PlaceInTheNearestCell(inventory.Container, this);
            //    }
            //    //inventory.AddCell(this);
            //    //EndDragExtension.PlaceInTheNearestCell(inventory.Container,
            //    //    this);
            //}
            //else
            //{
                EndDragExtension.PlaceInTheNearestCell(_originVisualContext,
                    this, currentIndex);
            //}

            _endDragEvent?.Invoke();
        }

        public void PlaceInTheNearestCell()
        {
            int closetIndex = 0;
            for (int i = 0; i < _originVisualContext.transform.childCount; i++)
            {
                if (Vector3.Distance(_originVisualContext.GetChild(i).position, transform.position) <
                    Vector3.Distance(_originVisualContext.GetChild(closetIndex).position, transform.position))
                {
                    closetIndex = i;
                }
            }
            transform.SetParent(_originVisualContext);
            transform.SetSiblingIndex(closetIndex);
        }

        public void Initialize(Transform globalVisualContext,
            InventoryItem inventoryItem, Action endDragEvent,
            Action<InventoryCell> beginDragEvent)
        {
            _globalVisualContext = globalVisualContext;
            _originVisualContext = transform.parent;
            InventoryItem = inventoryItem;
            _endDragEvent = endDragEvent;
            _beginDragEvent = beginDragEvent;
        }
    }
}
