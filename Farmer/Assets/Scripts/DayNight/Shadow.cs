using System;

using UnityEngine;

namespace Scripts.DayNight
{
    [DefaultExecutionOrder(999)]
    [ExecuteInEditMode]
    public class Shadow : MonoBehaviour
    {
        [Range(0, 10f)] public float BaseLength = 1f;

        private void OnEnable()
        {
            DayNightCycleHandler.RegisterShadow(this);
        }

        private void OnDisable()
        {
            DayNightCycleHandler.UnregisterShadow(this);
        }
    }
}
