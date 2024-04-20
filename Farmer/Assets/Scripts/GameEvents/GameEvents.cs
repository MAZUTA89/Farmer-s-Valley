using System;
using System.Collections.Generic;

namespace Scripts.FarmGameEvents
{
    public class GameEvents
    {
        public static Action OnExitTheGameEvent;

        public static void InvokeExitTheGameEvent()
        {
            OnExitTheGameEvent?.Invoke();
        }
    }
}
