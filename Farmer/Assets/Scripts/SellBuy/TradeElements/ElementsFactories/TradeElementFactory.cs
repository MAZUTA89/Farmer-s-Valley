using UnityEngine;
using Zenject;

namespace Scripts.SellBuy
{
    public class TradeElementFactory : ITradeElementFactory
    {
        TradeElement _tradeElementTemplate;
        IInstantiator _instantiator;
        public TradeElementFactory(TradeElement tradeElementTemplate, IInstantiator instantiator) 
        {
            _tradeElementTemplate = tradeElementTemplate;
            _instantiator = instantiator;
        }

        public TradeElement Create(Transform contentArea)
        {
            var element = _instantiator.InstantiatePrefabForComponent<TradeElement>(_tradeElementTemplate,
                contentArea);
            return element;
        }
    }
    public class SellTradeElementFactory : TradeElementFactory
    {
        public SellTradeElementFactory(TradeElement tradeElementTemplate,
            IInstantiator diContainer)
            : base(tradeElementTemplate, diContainer)
        {
        }
    }
    public class BuyTradeElementFactory : TradeElementFactory
    {
        public BuyTradeElementFactory(TradeElement tradeElementTemplate,
            IInstantiator diContainer)
            : base(tradeElementTemplate, diContainer)
        {
        }
    }
}
