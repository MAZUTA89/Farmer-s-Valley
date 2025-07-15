using System;
using UnityEngine;

namespace Scripts.SO.Chat
{
    [CreateAssetMenu(fileName ="ChatApiSO", menuName = "SO/ChatAssistant/ChatApiSO")]
    public class GptApiSO : ScriptableObject
    {
        public string APIKey;
        public string Url;
        public string ModelId;
    }
}
