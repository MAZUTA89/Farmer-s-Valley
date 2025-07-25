﻿using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using Zenject;
using Scripts.MainMenuCode;

namespace Scripts.Sounds
{
    public class SoundService : MonoBehaviour
    {
        [Serializable]
        public class SoundData
        {
            public float MainVolume = 1.0f;
            public float BGMVolume = 1.0f;
            public float SFXVolume = 1.0f;
        }

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioSource _UISource;
        [SerializeField] private AudioSource _sfxReferenceSource;

        public SoundData Sound { get; protected set; } = new();

        private Queue<AudioSource> m_SFXPool;
        SettingsMenu _settingsMenu;

        [Inject]
        public void Construct(SettingsMenu settingsMenu)
        {
            _settingsMenu = settingsMenu;
        }

        private void Awake()
        {

            const int PoolLength = 16;

            m_SFXPool = new Queue<AudioSource>();

            for (int i = 0; i < PoolLength; ++i)
            {
                GameObject obj = new GameObject("SFXPool");
                obj.transform.SetParent(transform);

                var source = Instantiate(_sfxReferenceSource);

                m_SFXPool.Enqueue(source);
            }
        }
        private void OnEnable()
        {
            if (!_settingsMenu.Controls) return;
            _settingsMenu?.AddListenerAtMainVolume(OnMainVolumeValueChange);
            _settingsMenu?.AddListenerAtMusicVolume(OnMusicVolumeValueChange);
            _settingsMenu?.AddListenerAtSFXVolume(OnSFXVolumeValueChange);
        }
        private void OnDisable()
        {
            if (!_settingsMenu.Controls) return;
            _settingsMenu?.RemoveListenerAtMainVolume(OnMainVolumeValueChange);
            _settingsMenu?.RemoveListenerAtMusicVolume(OnMusicVolumeValueChange);
            _settingsMenu?.RemoveListenerAtSFXVolume(OnSFXVolumeValueChange);
        }

        private void Start()
        {
            //Load();
        }

        public void UpdateVolume()
        {
            _mixer.SetFloat("MainVolume", Mathf.Log10(Mathf.Max(0.0001f, Sound.MainVolume)) * 30.0f);
            _mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(0.0001f, Sound.SFXVolume)) * 30.0f);
            _mixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Max(0.0001f, Sound.BGMVolume)) * 30.0f);
        }

        public void PlaySFXAt(Vector3 position, AudioClip clip, bool spatialized)
        {
            var source = m_SFXPool.Dequeue();

            source.clip = clip;
            source.transform.position = position;

            source.spatialBlend = spatialized ? 1.0f : 0.0f;

            source.Play();

            m_SFXPool.Enqueue(source);
        }

        public void PlayUISound()
        {
            _UISource.Play();
        }

        void OnMainVolumeValueChange(float value)
        {
            _mixer.SetFloat("MainVolume", Mathf.Log10(Mathf.Max(0.0001f, value)) * 30.0f);
        }
        void OnSFXVolumeValueChange(float value)
        {
            _mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(0.0001f, value)) * 30.0f);
        }

        void OnMusicVolumeValueChange(float value)
        {
            _mixer.SetFloat("BGMVolume", Mathf.Log10(Mathf.Max(0.0001f, value)) * 30.0f);
        }
    }
}
