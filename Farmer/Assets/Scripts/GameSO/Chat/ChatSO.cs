using UnityEngine;

namespace Scripts.SO.Chat
{
    [CreateAssetMenu(fileName ="ChatSO", menuName = "SO/ChatAssistant/ChatSO")]
    public class ChatSO : ScriptableObject
    {
        public string APIKey;
        public string Url;

        public string SystemMassage;
    }
}
