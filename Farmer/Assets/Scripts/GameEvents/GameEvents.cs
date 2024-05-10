using System;
using System.Collections.Generic;

namespace Scripts.FarmGameEvents
{
    public class GameEvents
    {
        public static Action OnExitTheGameEvent;
        public static Action OnSellItemEvent;
        public static Action<bool> OnTradePanelOpenClose;
       
        public static void InvokeExitTheGameEvent()
        {
            OnExitTheGameEvent?.Invoke();
        }

        public static void InvokeSellItemEvent()
        {
            OnSellItemEvent?.Invoke();
        }

        public static void InvokeTradePanelActionEvent(bool isIgnore)
        {
            OnTradePanelOpenClose?.Invoke(isIgnore);
        }
    }
}
