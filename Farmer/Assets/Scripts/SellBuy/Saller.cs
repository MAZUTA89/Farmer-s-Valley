using Scripts.FarmGameEvents;
using Scripts.SellBuy;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.SellBuy
{
    public class Seller : MonoBehaviour
    {
        [SerializeField] private GameObject _panelObject;
        [SerializeField] private TradePanel TradePanel;
        [SerializeField] private GameObject ChatObject;

        private void OnMouseDown()
        {
            if(_panelObject.gameObject.activeSelf == false)
            {
                _panelObject.gameObject.SetActive(true);
            }
            else
            {
                _panelObject.gameObject.SetActive(false);
            }
            GameEvents.InvokeTradePanelActionEvent(!_panelObject.gameObject.activeSelf);
        }

        public void OnTrade()
        {
            TradePanel.gameObject.SetActive(true);
            _panelObject.gameObject.SetActive(false);
        }

        public void OnTalk()
        {
            ChatObject.SetActive(true);
            _panelObject.gameObject.SetActive(false);
        }
    }
}
