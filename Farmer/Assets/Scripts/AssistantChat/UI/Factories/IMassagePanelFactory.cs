
using UnityEngine;

namespace Scripts.ChatAssistant
{
    public interface IMassagePanelFactory
    {
        MassagePanel CreateMassagePanel(Transform parent);
    }
}
