using System.Collections.Generic;
using UnityEngine;

namespace Scripts.ChatAssistant
{
    public enum MassagePanelType
    {
        Player,
        Assistant
    }
    public class MassagePanelsFactories
    {
        Dictionary<MassagePanelType, IMassagePanelFactory> _massagePanels; 
        public MassagePanelsFactories()
        {
            _massagePanels = new Dictionary<MassagePanelType, IMassagePanelFactory>();
        }

        public void Add(MassagePanelType type, IMassagePanelFactory factory)
        {
            if(_massagePanels.TryAdd(type, factory) == false)
            {
                Debug.LogError($"Failed to add {typeof(IMassagePanelFactory)} as {type}");
            } 
        }

        public IMassagePanelFactory GetByType(MassagePanelType type)
        {
            if (_massagePanels.TryGetValue(type, out IMassagePanelFactory massagePanelFactory))
            {
                return massagePanelFactory;
            }
            else
                return default;
        }
    }
}
