using Scripts.FarmGameEvents;
using Scripts.Inventory;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


namespace Scripts.SellBuy
{
    public class SellTradeElement : TradeElement
    {
        ItemContextData _itemData;
        public override async void OnButtonAction()
        {
            if (Trade())
                Destroy(gameObject);
            //StartCoroutine(OverriteSellList(5f));
            await OverriteSellList();
        }
        public override bool Trade()
        {
            TradeService.Sell(_itemData);
            return true;
        }
        public void InitializeItemContext(ItemContextData contextData)
        {
            _itemData = contextData;
        }

        IEnumerator OverriteSellList(float time)
        {
            
            yield return new WaitForSeconds(time);
            Debug.Log(time);
            GameEvents.InvokeSellItemEvent();
        }
        async Task OverriteSellList()
        {

            await Task.Delay(500);
            Debug.Log(500);
            GameEvents.InvokeSellItemEvent();
        }
    }
}
