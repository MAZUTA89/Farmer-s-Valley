using Scripts.SellBuy;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.SellBuy
{
    public class Seller : MonoBehaviour
    {
        [SerializeField] private TradePanel _panelObject;

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
    }
}
