using Scripts.InventoryCode;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Scripts.SellBuy
{
    public abstract class TradeElement : MonoBehaviour
    {
        public Image IconElement => _iconElementField;
        public TextMeshProUGUI IconName => _iconNameField;
        public TextMeshProUGUI ButtonText => _buttonTextField;

        protected Button ButtonElement => _buttonElementField;

        [SerializeField] private Image _iconElementField;
        [SerializeField] private TextMeshProUGUI _iconNameField;
        [SerializeField] private TextMeshProUGUI _buttonTextField;
        [SerializeField] private Button _buttonElementField;

        protected TradeService TradeService;

        protected InventoryItem Item;

        [Inject]
        public void Construct(TradeService tradeService)
        {
            TradeService = tradeService;
        }
        public void Init(InventoryItem item)
        {
            Item = item;
        }
        public virtual void OnButtonAction()
        {
            if(Trade())
                Destroy(gameObject);
        }

        
        public abstract bool Trade();
    }
}
