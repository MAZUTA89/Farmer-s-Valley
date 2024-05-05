using System;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.DayNight
{
    [DefaultExecutionOrder(-10)]
    public class DayNightService : MonoBehaviour
    {
        private static DayNightService s_instance;
        private DayNightCycleHandler _dayCycleHandler;
        [Header("Time settings")]
        [Min(1.0f)]
        public float DayDurationInSeconds;

        public float StartingTime = 0.0f;
        // Will return the ratio of time for the current day between 0 (00:00) and 1 (23:59).
        public float CurrentDayRatio => m_CurrentTimeOfTheDay / DayDurationInSeconds;
        private float m_CurrentTimeOfTheDay;
        public float CurrentTimeOfTheDay;
        private bool m_IsTicking;

        private List<DayNightEventHandler> m_EventHandlers = new();

        private void Awake()
        {
            if(s_instance == null)
            {
                s_instance = this;
            }
        }

        private void Start()
        {
            m_IsTicking = true;
            _dayCycleHandler = GetComponent<DayNightCycleHandler>();
        }


        private void Update()
        {
            CurrentTimeOfTheDay = m_CurrentTimeOfTheDay;
            if (m_IsTicking)
            {
                float previousRatio = CurrentDayRatio;
                m_CurrentTimeOfTheDay += Time.deltaTime;

                while (m_CurrentTimeOfTheDay > DayDurationInSeconds)
                    m_CurrentTimeOfTheDay -= DayDurationInSeconds;

                foreach (var handler in m_EventHandlers)
                {
                    foreach (var evt in handler.Events)
                    {
                        bool prev = evt.IsInRange(previousRatio);
                        bool current = evt.IsInRange(CurrentDayRatio);

                        if (prev && !current)
                        {
                            evt.OffEvent.Invoke();
                        }
                        else if (!prev && current)
                        {
                            evt.OnEvents.Invoke();
                        }
                    }
                }

                if (_dayCycleHandler != null)
                    _dayCycleHandler.Tick(CurrentDayRatio);
            }
        }
        public static int GetHourFromRatio(float ratio)
        {
            var time = ratio * 24.0f;
            var hour = Mathf.FloorToInt(time);

            return hour;
        }

        public static int GetMinuteFromRatio(float ratio)
        {
            var time = ratio * 24.0f;
            var minute = Mathf.FloorToInt((time - Mathf.FloorToInt(time)) * 60.0f);

            return minute;
        }

        /// <summary>
        /// Return in the format "xx:xx" the given ration (between 0 and 1) of time
        /// </summary>
        /// <param name="ratio"></param>
        /// <returns></returns>
        public static string GetTimeAsString(float ratio)
        {
            var hour = GetHourFromRatio(ratio);
            var minute = GetMinuteFromRatio(ratio);

            return $"{hour}:{minute:00}";
        }
        /// <summary>
        /// Will return the current time as a string in format of "xx:xx" 
        /// </summary>
        /// <returns></returns>
        public string CurrentTimeAsString()
        {
            return GetTimeAsString(CurrentDayRatio);
        }
        public static void RegisterEventHandler(DayNightEventHandler handler)
        {
            foreach (var evt in handler.Events)
            {
                if (evt.IsInRange(s_instance.CurrentDayRatio))
                {
                    evt.OnEvents.Invoke();
                }
                else
                {
                    evt.OffEvent.Invoke();
                }
            }

             s_instance.m_EventHandlers.Add(handler);
        }

        public static void RemoveEventHandler(DayNightEventHandler handler)
        {
            s_instance.m_EventHandlers.Remove(handler);
        }
    }
}
