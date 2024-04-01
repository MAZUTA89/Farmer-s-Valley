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
        List<InventoryItemSO> _inventoryItems;
        Transform _dragParent;
        Transform _dragOrigin;
        public bool IsEmpty {  get; private set; }

        public Image Icon => IconElement;
        public TextMeshProUGUI Text => TextElement;
        private void Start()
        {
        }
        public void Initialize(Transform dragParent, List<InventoryItemSO> inventoryItems)
        {
            _dragOrigin = transform.parent;
            _dragParent = dragParent;
            _inventoryItems = inventoryItems;
        }
        public void SetEmpty(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }

        public void RenderCell()
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log($"Draging {gameObject.name}");
            transform.position = Input.mousePosition;
            //Debug.Log("Drag index element:" + )
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log($"Begin Drag {gameObject.name}");
            transform.SetParent(_dragParent);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log($"End Drag {gameObject.name}");
            int closetIndex = 0;
            for (int i = 0; i < _dragOrigin.transform.childCount; i++)
            {
                if(Vector3.Distance(_dragOrigin.GetChild(i).position, transform.position) <
                    Vector3.Distance(_dragOrigin.GetChild(closetIndex).position, transform.position))
                {
                    closetIndex = i;
                }

            }

            //_inventoryItems.In
            transform.SetParent(_dragOrigin);
            Debug.Log("Closet index " + closetIndex);
            transform.SetSiblingIndex(closetIndex);
        }
    }
}
