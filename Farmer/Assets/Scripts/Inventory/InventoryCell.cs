﻿using PimDeWitte.UnityMainThreadDispatcher;
using Scripts.InventoryCode.ItemResources;
using Scripts.MouseHandle;
using System;
using System.Threading.Tasks;
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

        public Transform GlobalVisualContext { get; private set; }
        public Transform OriginVisualContext { get; private set; }

        public Image Icon => IconElement;
        public TextMeshProUGUI NameDisplayText => TextElement;
        public TextMeshProUGUI CountText => CountTextElement;
        public Image SelectIcon => SelectIconElement;

        public int BeginDragSiblingIndex { get; private set; }

        ItemResourceDroper _itemResourceDroper;
        MouseCursor _mouseCursor;

        [Inject]
        public void Construct(ItemResourceDroper itemResourceDroper,
            MouseCursor mouseCursor)
        {
            _itemResourceDroper = itemResourceDroper;
            _mouseCursor = mouseCursor;
        }


        private void OnDisable()
        {
            
        }
        private void Start()
        {
        }
        public void Initialize(Transform globalVisualContext,
            InventoryItem inventoryItem)
        {
            GlobalVisualContext = globalVisualContext;
            OriginVisualContext = transform.parent;
            InventoryItem = inventoryItem;
            
        }
        public void RegisterEvents(Action endDragEvent, Action<InventoryCell> beginDragEvent)
        {
            _endDragEvent = endDragEvent;
            _beginDragEvent = beginDragEvent;
        }
        private async void Update()
        {
            InventoryItem?.RenderUI(this);
            if(InventoryItem.Consumable &&
                InventoryItem.Count < 1)
            {
                Destroy(gameObject);
                await InvokeEndDragOnItemOver();
            }
        }

        async Task InvokeEndDragOnItemOver()
        {
            await Task.Delay(100);
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                Debug.Log("InvokeEndDragOnItemOver");
                _endDragEvent?.Invoke();
            });
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDragSiblingIndex = transform.GetSiblingIndex();
            _beginDragEvent?.Invoke(this);
            transform.SetParent(GlobalVisualContext);
            _mouseCursor.ChangeCursor(CursorType.Drag);
        }

        public async void OnEndDrag(PointerEventData eventData)
        {
            _mouseCursor.ChangeCursor(CursorType.Default);
            InventoryBase inventory;
            if (DragExtension.CheckMouseIntersectionWithContainers(eventData,
                out inventory))// если есть пересечение с инвентарем
            {
                // если это тот же инвентарь
                if (OriginVisualContext == inventory.Container)
                {
                    await DragExtension.PlaceInTheNearestCellLocal(OriginVisualContext,
                    this, BeginDragSiblingIndex).ContinueWith((i) =>
                    {
                        UnityMainThreadDispatcher.Instance().Enqueue(() =>
                        {
                            _endDragEvent?.Invoke();

                        });
                    });
                }
                else//другой инвентарь
                {
                    if (inventory.IsFull())// если полон, то не перекладывать
                    {
                        await DragExtension.PlaceInTheNearestCellLocal(OriginVisualContext,
                        this, BeginDragSiblingIndex).ContinueWith((i) =>
                        {
                            UnityMainThreadDispatcher.Instance().Enqueue(() =>
                            {
                                _endDragEvent?.Invoke();

                            });
                        });
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
