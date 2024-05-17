using System;
using System.Collections.Generic;

namespace Scripts.FarmGameEvents
{
    public class GameEvents
    {
        public static Action OnExitTheGameEvent;
        public static Action OnSellItemEvent;
        public static Action<bool> OnTradePanelOpenClose { get; set; }
        public static Action OnSaveSettingsEvent;
        public static Action<string, bool> OnPerformInteractiveRebindEvent;
       
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

        public static void InvokeOnSaveSettingsEvent()
        {
            OnSaveSettingsEvent?.Invoke();
        }

        public static void InvokeOnPerformInteractiveRebindEvent(string text, bool activeSelf)
        {
            OnPerformInteractiveRebindEvent?.Invoke(text, activeSelf);
        }
    }
}
