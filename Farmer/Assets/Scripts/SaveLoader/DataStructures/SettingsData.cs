using UnityEngine;


namespace Scripts.SaveLoader
{
    public class SettingsData
    {
        public int ResolutionWidth;
        public int ResolutionHeight;
        public bool IsFoolScreen;
        public float MainVolume;
        public float MusicVolume;
        public float SFXVolume;
        public string InputOverrides;

        public void SetResolution(Resolution resolution)
        {
            ResolutionWidth = resolution.width;
            ResolutionHeight = resolution.height;
        }
        public Resolution GetResolution()
        {
            Resolution resolution = new Resolution();
            resolution.width = ResolutionWidth;
            resolution.height = ResolutionHeight;
            return resolution;
        }
    }
}
