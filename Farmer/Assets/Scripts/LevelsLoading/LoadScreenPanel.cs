using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.LevelsLoading
{
    public class LoadScreenPanel : MonoBehaviour
    {
        [SerializeField] private Slider _progressSlider;
        public Slider ProgressSlider => _progressSlider;
       

        public void Activate()
        {
            gameObject.SetActive(true);
            
            _progressSlider.value = 0;
        }
        public void Deactivate()
        {
            gameObject.SetActive(false);
            
            _progressSlider.value = 0;
        }
    }
}
