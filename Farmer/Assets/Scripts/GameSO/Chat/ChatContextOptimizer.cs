using System;
using SharpToken;
using UnityEngine;

namespace Assets.Scripts.GameSO.Chat
{
    public class ChatContextOptimizer
    {
        string _encodingModelId;
        int _restContextSize;
        GptEncoding _gptEncoding;
        public ChatContextOptimizer(string encodingModelId, int contextSize) 
        {
            _encodingModelId = encodingModelId;
            _restContextSize = contextSize;
        }

        public bool TryInitialize()
        {
            try
            {
                _gptEncoding = GptEncoding.GetEncoding(_encodingModelId);

                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);

                return false;
            }
        }

        public bool IsNeedReset(string text)
        {
            var encodedCount = _gptEncoding.CountTokens(text);

            Debug.Log($"Current context size: {encodedCount}");

            return encodedCount >= _restContextSize;
        }
    }
}
