
namespace Scripts.SellBuy
{
    public class BuyTradeElement : TradeElement
    {
        public override bool Trade()
        {
            TradeService.Buy(Item, 1);
            return false;
        }
        public void Update()
        {
            ButtonElement.interactable = TradeService.BuyCondition(Item);
        }
    }
}
