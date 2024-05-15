using System;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using Zenject;

namespace Scripts.ChatAssistant
{
    public class ChatPanel : MonoBehaviour
    {
        Transform _container;
        MassagePanelsFactories _massagePanelsFactories;
        [Inject]
        public void Construct(MassagePanelsFactories massagePanelsFactories)
        {
            _massagePanelsFactories = massagePanelsFactories;
        }

        private void Start()
        {
            _container = gameObject.transform;
        }

        public async void CreateMassagePanel(IMassagePanelFactory massagePanelFactory, string text)
        {
            MassagePanel massagePanel = massagePanelFactory.CreateMassagePanel(_container);

            ResizeMassagePanel(massagePanel, text);

            await massagePanel.WriteText(text);
        }

        void ResizeMassagePanel(MassagePanel massagePanel, string text)
        {
            int charCount = text.Length;
            float stringCount = charCount * massagePanel.FontSize / massagePanel.Size.x;

            float panelHeight = stringCount * massagePanel.FontSize;

            massagePanel.Size = new Vector2(massagePanel.Size.x, panelHeight);
        }
        
    }
}
