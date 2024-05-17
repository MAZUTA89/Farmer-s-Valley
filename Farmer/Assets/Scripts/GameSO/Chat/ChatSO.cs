using System;
using UnityEngine;

namespace Scripts.SO.Chat
{
    [CreateAssetMenu(fileName ="ChatSO", menuName = "SO/ChatAssistant/ChatSO")]
    public class ChatSO : ScriptableObject
    {
        public string APIKey;
        public string Url;
        public string ModelId;
        [Multiline]
        public string SystemMassage;
        [Space]
        [Header("Request parameters")]
        [Range(0f, 10f)]
        public double Temperature;
    }
}
