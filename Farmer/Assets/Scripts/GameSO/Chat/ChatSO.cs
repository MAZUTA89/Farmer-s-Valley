using Scripts.InventoryCode;
using System;
using UnityEngine;

namespace Scripts.SO.Chat
{
    [CreateAssetMenu(fileName ="ChatSO", menuName = "SO/ChatAssistant/ChatSO")]
    public class ChatSO : ScriptableObject, IDataBaseItem
    {
        public string Name;
        public string APIKey;
        public string Url;
        public string ModelId;
        [Multiline]
        public string SystemMassage;
        [Space]
        [Header("Request parameters:")]
        [Range(0f, 1f)]
        public double Temperature;
        [Header("Сколько токенов использовать для ответа")]
        [Range(0, 200)]
        public int MaxTokens;
        [Header("Название модели токенизатора")]
        public string EncodingModelID;
        [Header("Величина активного сеанса для очистки")]
        [Range(1500, 20000)]
        public int ResetContextSize;

        public string UniqueName => Name;
    }
}
