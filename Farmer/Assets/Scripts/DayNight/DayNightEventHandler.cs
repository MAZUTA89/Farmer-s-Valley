using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


namespace Scripts.DayNight
{
    [DefaultExecutionOrder(999)]
    public class DayNightEventHandler : MonoBehaviour
    {
        [System.Serializable]
        public class DayEvent
        {
            public float StartTime = 0.0f;
            public float EndTime = 1.0f;

            public UnityEvent OnEvents;
            public UnityEvent OffEvent;

            public bool IsInRange(float t)
            {
                return t >= StartTime && t <= EndTime;
            }
        }

        public DayEvent[] Events;

        private void Start()
        {
            DayNightService.RegisterEventHandler(this);
        }

        private void OnDisable()
        {
            DayNightService.RemoveEventHandler(this);
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DayNightEventHandler.DayEvent))]
    public class DayEvent : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var dayHandler = GameObject.FindObjectOfType<DayNightCycleHandler>();

            // Create property container element.
            var container = new VisualElement();

            if (dayHandler != null)
            {
                var minProperty = property.FindPropertyRelative(nameof(DayNightEventHandler.DayEvent.StartTime));
                var maxProperty = property.FindPropertyRelative(nameof(DayNightEventHandler.DayEvent.EndTime));

                var slider = new MinMaxSlider(
                    $"Day range {DayNightService.GetTimeAsString(minProperty.floatValue)} - {DayNightService.GetTimeAsString(maxProperty.floatValue)}",
                    minProperty.floatValue, maxProperty.floatValue, 0.0f, 1.0f);

                slider.RegisterValueChangedCallback(evt =>
                {
                    minProperty.floatValue = evt.newValue.x;
                    maxProperty.floatValue = evt.newValue.y;

                    property.serializedObject.ApplyModifiedProperties();

                    slider.label =
                        $"Day range {DayNightService.GetTimeAsString(minProperty.floatValue)} - {DayNightService.GetTimeAsString(maxProperty.floatValue)}";
                });

                var evtOnProperty = property.FindPropertyRelative(nameof(DayNightEventHandler.DayEvent.OnEvents));
                var evtOffProperty = property.FindPropertyRelative(nameof(DayNightEventHandler.DayEvent.OffEvent));

                container.Add(slider);

                container.Add(new PropertyField(evtOnProperty, "On Event"));
                container.Add(new PropertyField(evtOffProperty, "Off Event"));
            }
            else
            {
                container.Add(new Label("There is no DayCycleHanlder in the scene and it is needed"));
            }

            return container;
        }
    }
#endif
}
