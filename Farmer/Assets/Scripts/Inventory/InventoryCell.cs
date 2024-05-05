using Scripts.InventoryCode.ItemResources;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;


namespace Scripts.InventoryCode
{
    public class InventoryCell : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] Image IconElement;
        [SerializeField] TextMeshProUGUI TextElement;
        [SerializeField] TextMeshProUGUI CountTextElement;
        [SerializeField] Image SelectIconElement;
        Action _endDragEvent;
        Action<InventoryCell> _beginDragEvent;
        public InventoryItem InventoryItem { get; private set; }

        public Transform _globalVisualContext { get; private set; }
        public Transform OriginVisualContext { get; private set; }

        public Image Icon => IconElement;
        public TextMeshProUGUI NameDisplayText => TextElement;
        public TextMeshProUGUI CountText => CountTextElement;
        public Image SelectIcon => SelectIconElement;

        public int BeginDragSiblingIndex { get; private set; }

        ItemResourceDroper _itemResourceDroper;

        [Inject]
        public void Construct(ItemResourceDroper itemResourceDroper)
        {
            _itemResourceDroper = itemResourceDroper;
        }


        private void OnDisable()
        {
            _endDragEvent = null;
        }
        private void Start()
        {

        }
        public void Initialize(Transform globalVisualContext,
            InventoryItem inventoryItem)
        {
            _globalVisualContext = globalVisualContext;
            OriginVisualContext = transform.parent;
            InventoryItem = inventoryItem;
            
        }
        public void RegisterEvents(Action endDragEvent, Action<InventoryCell> beginDragEvent)
        {
            _endDragEvent = endDragEvent;
            _beginDragEvent = beginDragEvent;
        }
        private void Update()
        {
            InventoryItem?.RenderUI(this);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragSiblingIndex = transform.GetSiblingIndex();
            _beginDragEvent?.Invoke(this);
            transform.SetParent(_globalVisualContext);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            InventoryBase inventory;
            if (DragExtension.CheckMouseIntersectionWithContainers(eventData,
                out inventory))// если есть пересечение с другим инвентарем
            {
                // если это тот же инвентарь
                if (OriginVisualContext == inventory.Container)
                {
                    DragExtension.PlaceInTheNearestCellLocal(OriginVisualContext,
                    this, BeginDragSiblingIndex);
                }
                else//другой инвентарь
                {
                    if (inventory.IsFull())// если полон, то не перекладывать
                    {
                        DragExtension.PlaceInTheNearestCellLocal(OriginVisualContext,
                        this, BeginDragSiblingIndex);
                    }
                    else// переложить и переподписать ячейку на события другого инвентаря
                    {
                        DragExtension.PlaceInTheNearestCellGlobal(inventory.Container, this);
                        OriginVisualContext = inventory.Container;
                        _endDragEvent?.Invoke();
                        inventory.RegisterDragEvents(this);
                        _endDragEvent?.Invoke();
                        inventory.OnDragInto(this);
                        return;
                    }
                }
            }
            else// нет пересечений с другим инвентарем
            {
                _endDragEvent?.Invoke();
                Destroy(this.gameObject);
                _itemResourceDroper.DropByPlayer(InventoryItem);
            }
            _endDragEvent?.Invoke();
        }
    }
}
