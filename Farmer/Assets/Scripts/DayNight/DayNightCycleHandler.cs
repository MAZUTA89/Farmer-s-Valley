using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

namespace Scripts.DayNight
{
    public class DayNightCycleHandler : MonoBehaviour
    {
        private static DayNightCycleHandler s_instance;
        public Transform LightsRoot;

        [Header("Day Light")]
        public Light2D DayLight;
        public Gradient DayLightGradient;

        [Header("Night Light")]
        public Light2D NightLight;
        public Gradient NightLightGradient;

        [Header("Ambient Light")]
        public Light2D AmbientLight;
        public Gradient AmbientLightGradient;

        [Header("RimLights")]
        public Light2D SunRimLight;
        public Gradient SunRimLightGradient;
        public Light2D MoonRimLight;
        public Gradient MoonRimLightGradient;

        [Tooltip("The angle 0 = upward, going clockwise to 1 along the day")]
        public AnimationCurve ShadowAngle;
        [Tooltip("The scale of the normal shadow length (0 to 1) along the day")]
        public AnimationCurve ShadowLength;

        public List<Shadow> m_Shadows { get; set; } = new();
        private List<FarmerLightInterpolator> m_LightBlenders = new();

        private void Awake()
        {
            if(s_instance == null)
            {
                s_instance = this;
            }
        }

        public void Tick(float ratio)
        {
            UpdateLight(ratio);
        }

        public void UpdateLight(float ratio)
        {
            DayLight.color = DayLightGradient.Evaluate(ratio);
            NightLight.color = NightLightGradient.Evaluate(ratio);

#if UNITY_EDITOR
            //the test between the define will only happen in editor and not in build, as it is assumed those will be set
            //in build. But in editor we may want to test without those set. (those were added later in development so
            //some test scene didn't have those set and we wanted to be able to still test those)
            if (AmbientLight != null)
#endif
                AmbientLight.color = AmbientLightGradient.Evaluate(ratio);

#if UNITY_EDITOR
            if (SunRimLight != null)
#endif
                SunRimLight.color = SunRimLightGradient.Evaluate(ratio);

#if UNITY_EDITOR
            if (MoonRimLight != null)
#endif
                MoonRimLight.color = MoonRimLightGradient.Evaluate(ratio);

            LightsRoot.rotation = Quaternion.Euler(0, 0, 360.0f * ratio);

            UpdateShadow(ratio);
        }

        void UpdateShadow(float ratio)
        {
            var currentShadowAngle = ShadowAngle.Evaluate(ratio);
            var currentShadowLength = ShadowLength.Evaluate(ratio);

            var opposedAngle = currentShadowAngle + 0.5f;
            while (currentShadowAngle > 1.0f)
                currentShadowAngle -= 1.0f;

            foreach (var shadow in m_Shadows)
            {
                var t = shadow.transform;
                //use 1.0-angle so that the angle goes clo
                t.eulerAngles = new Vector3(0, 0, currentShadowAngle * 360.0f);
                t.localScale = new Vector3(1, 1f * shadow.BaseLength * currentShadowLength, 1);
            }

            foreach (var handler in m_LightBlenders)
            {
                handler.SetRatio(ratio);
            }
        }

        public static void RegisterShadow(Shadow shadow)
        {
#if UNITY_EDITOR
            //in the editor when not running, we find the instance manually. Less efficient but not a problem at edit time
            //allow to be able to previz shadow in editor 
            if (!Application.isPlaying)
            {
                var instance = GameObject.FindObjectOfType<DayNightCycleHandler>();
                if (instance != null)
                {
                   instance.m_Shadows.Add(shadow);
                }
            }
            else
            {
#endif
               s_instance.m_Shadows.Add(shadow);
#if UNITY_EDITOR
            }
#endif
        }
        public static void UnregisterShadow(Shadow shadow)
        {
#if UNITY_EDITOR
            //in the editor when not running, we find the instance manually. Less efficient but not a problem at edit time
            //allow to be able to previz shadow in editor 
            if (!Application.isPlaying)
            {
                var instance = GameObject.FindObjectOfType<DayNightCycleHandler>();
                if (instance != null)
                {
                    instance.m_Shadows.Remove(shadow);
                }
            }
            else
            {
#endif
                if (s_instance != null)
                    s_instance.m_Shadows.Remove(shadow);
#if UNITY_EDITOR
            }
#endif
        }

        public static void RegisterLightBlender(FarmerLightInterpolator interpolator)
        {
#if UNITY_EDITOR
            //in the editor when not running, we find the instance manually. Less efficient but not a problem at edit time
            //allow to be able to previz shadow in editor 
            if (!Application.isPlaying)
            {
                var instance = FindObjectOfType<DayNightCycleHandler>();
                if (instance != null)
                {
                    instance.m_LightBlenders.Add(interpolator);
                }
            }
            else
            {
#endif
                s_instance.m_LightBlenders.Add(interpolator);
#if UNITY_EDITOR
            }
#endif
        }

        public static void UnregisterLightBlender(FarmerLightInterpolator interpolator)
        {
#if UNITY_EDITOR
            //in the editor when not running, we find the instance manually. Less efficient but not a problem at edit time
            //allow to be able to previz shadow in editor 
            if (!Application.isPlaying)
            {
                var instance = FindObjectOfType<DayNightCycleHandler>();
                if (instance != null)
                {
                    instance.m_LightBlenders.Remove(interpolator);
                }
            }
            else
            {
#endif
                if (s_instance != null)
                    s_instance.m_LightBlenders.Remove(interpolator);
#if UNITY_EDITOR
            }
#endif
        }
    }
    [System.Serializable]
    public struct DayCycleHandlerSaveData
    {
        public float TimeOfTheDay;
    }


#if UNITY_EDITOR
    // Wrapping a custom editor between UNITY_EDITOR define check allow to keep it in the same 
    // file as this part will be stripped when building for standalone (where Editor class doesn't exist).
    // Don't forget to also wrap the UnityEditor using at the top of the file between those define check too.

    // Show a slider that allow to test a specific time to help define colors.
    [CustomEditor(typeof(DayNightCycleHandler))]
    class DayCycleEditor : Editor
    {
        private DayNightCycleHandler m_Target;

        public override VisualElement CreateInspectorGUI()
        {
            m_Target = target as DayNightCycleHandler;

            var root = new VisualElement();

            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            var slider = new Slider(0.0f, 1.0f);
            slider.label = "Test time 0:00";
            slider.RegisterValueChangedCallback(evt =>
            {
                m_Target.UpdateLight(evt.newValue);

                slider.label = $"Test Time {DayNightService.GetTimeAsString(evt.newValue)} ({evt.newValue:F2})";
                SceneView.RepaintAll();
            });

            //registering click event, it's very catch all but not way to do a change check for control change
            root.RegisterCallback<ClickEvent>(evt =>
            {
                m_Target.UpdateLight(slider.value);
                SceneView.RepaintAll();
            });

            root.Add(slider);

            return root;
        }
    }
#endif
}
