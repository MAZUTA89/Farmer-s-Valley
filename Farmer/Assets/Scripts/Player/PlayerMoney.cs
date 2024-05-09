using System;
using System.Collections.Generic;
using TMPro;

namespace Scripts.PlayerCode
{
    public class PlayerMoney
    {
        TextMeshProUGUI _moneyText;
        public PlayerMoney(int startMoney, 
            TextMeshProUGUI moneyText)
        {
            _moneyText = moneyText;
            Money = startMoney;
        }
          
        const int c_maxMoneyValue = 9999;
        const int c_minMoneyValue = 0;
        public int Money
        {
            get
            {
                return _money;
            }
            set
            {
                _money = value;
                if(_money < 0)
                    _money = c_minMoneyValue;
                if(_money > c_maxMoneyValue)
                    _money = c_maxMoneyValue;
                _moneyText.text = _money.ToString();
            }
        }

        int _money;
    }
}
