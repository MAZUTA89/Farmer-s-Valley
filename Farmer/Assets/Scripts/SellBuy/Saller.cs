using Scripts.FarmGameEvents;
using Scripts.SellBuy;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Scripts.SellBuy
{
    public class Seller : MonoBehaviour
    {
        [SerializeField] private GameObject _panelObject;
        [SerializeField] private GameObject TradePanel;
        [SerializeField] private GameObject ChatObject;
        InputService _inputService;

        [Inject]
        public void Construct(InputService inputService)
        {
            _inputService = inputService;
        }

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
        }

        public void OnTrade()
        {
            _inputService?.LockGamePlayControls();
            TradePanel.SetActive(true);
            _panelObject.gameObject.SetActive(false);
            GameEvents.InvokeTradePanelActionEvent(!_panelObject.gameObject.activeSelf);
        }

        public void OnTalk()
        {
            _inputService?.LockGamePlayControls();
            ChatObject.SetActive(true);
            _panelObject.gameObject.SetActive(false);
        }
    }
}
