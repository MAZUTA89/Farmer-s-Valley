using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zenject;
using Scripts.InteractableObjects;
using Scripts.Inventory;
using Scripts.FarmGameEvents;
using UnityEngine.EventSystems;
using Scripts.MouseHandle;

namespace Scripts.SellBuy
{
    public class TradePanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] Transform ContentArea;

        TradeService _tradeService;
        PlayerInventory _playerInventory;
        StringBuilder _buttonTextBuilder = new StringBuilder();
        FactoriesProvider _factoriesProvider;
        ITradeElementFactory _sellElementFactory;
        ITradeElementFactory _buyElementFactory;
        BuyItemsDatabase _buyItemsDatabase;
        MouseCursor _mouseCursor;

        [Inject]
        public void Construct(TradeService tradeService,
            PlayerInventory playerInventory,
            FactoriesProvider factoriesProvider,
            BuyItemsDatabase buyItemsDatabase,
            MouseCursor mouseCursor)
        {
            _mouseCursor = mouseCursor;
            _tradeService = tradeService;
            _playerInventory = playerInventory;
            _factoriesProvider = factoriesProvider;
            _buyItemsDatabase = buyItemsDatabase;
        }
        private void OnEnable()
        {
            GameEvents.OnSellItemEvent += OnSellItem;
        }
        private void OnDisable()
        {
            GameEvents.OnSellItemEvent -= OnSellItem;
        }
        private void Start()
        {
            _sellElementFactory =
                (SellTradeElementFactory)_factoriesProvider.GetFactory<SellTradeElementFactory>();
            _buyElementFactory =
                (BuyTradeElementFactory)_factoriesProvider.GetFactory<BuyTradeElementFactory>();
            gameObject.SetActive(false);
        }

        public void OnBuy()
        {
            ClearElements();
            List<InventoryItem> inventoryItems =
                _playerInventory.GetAllItems();
            foreach (var item in _buyItemsDatabase.Items)
            {

                var UIElement = _buyElementFactory.Create(ContentArea);
                var itemClone = item.Clone() as InventoryItem;
                UIElement.Init(itemClone);
                UIElement.IconElement.sprite = item.Icon;
                UIElement.IconName.text = item.DisplayName;

                int amount = _tradeService.ItemAmount(item);
                _buttonTextBuilder.Append($"Spend {item.BuyPrice} " +
                    $"for {itemClone.Count}");

                UIElement.ButtonText.text = _buttonTextBuilder.ToString();

                _buttonTextBuilder.Clear();
            }
        }
        public void OnSell()
        {
            ClearElements();

            var itemsContext = _playerInventory.GetAllItemsContextData();

            DebugItemContextData(itemsContext);

            foreach (ItemContextData contextData in itemsContext)
            {
                if (_tradeService.SellCondition(contextData.Item))
                {
                    var UIElement = _sellElementFactory.Create(ContentArea);

                    if( UIElement is SellTradeElement tradeElement)
                    {
                        tradeElement.InitializeItemContext(contextData);
                        tradeElement.Init(contextData.Item.Clone() as InventoryItem);
                    }

                    
                    UIElement.IconElement.sprite = contextData.Item.Icon;
                    UIElement.IconName.text = contextData.Item.DisplayName;

                    Debug.Log("Ставлю :" +  contextData.Item.DisplayName);

                    _buttonTextBuilder.Append($"Take {contextData.Item.Count * contextData.Item.BuyPrice} " +
                        $"for {contextData.Item.Count}");

                    UIElement.ButtonText.text = _buttonTextBuilder.ToString();

                    _buttonTextBuilder.Clear();
                }
            }
        }
        void DebugItemContextData(List<ItemContextData> itemContextDatas)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in itemContextDatas)
            {
                stringBuilder.Append("Get " + item.Item.DisplayName + " ");
            }
            Debug.Log(stringBuilder.ToString());
        }
        public void OnExit()
        {
            ClearElements();
            gameObject.SetActive(false);
            GameEvents.InvokeTradePanelActionEvent(true);
        }
        void ClearElements()
        {
            for (int i = 0; i < ContentArea.childCount; i++)
            {
                Destroy(ContentArea.GetChild(i).gameObject);
            }
        }

        void OnSellItem()
        {
            OnSell();
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 mousePos = Input.mousePosition;
            
            gameObject.transform.position = mousePos;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _mouseCursor.ChangeCursor(CursorType.Drag);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _mouseCursor.ChangeCursor(CursorType.Default);
        }
    }
}
