using PimDeWitte.UnityMainThreadDispatcher;
using Scripts.SO.Chat;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.ChatAssistant
{
    public class MassagePanel : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _childRect;
        [SerializeField] private RectTransform _textRect;
        [SerializeField] private TextMeshProUGUI _textField;
        public TextMeshProUGUI TextField => _textField;
        public float FontSize => TextField.fontSize;
        public Vector2 Size
        {
            get
            {
                return _rectTransform.rect.size;
            }
            set
            {
                _rectTransform.sizeDelta = new Vector2(value.x, value.y);
                _childRect.sizeDelta = new Vector2(_childRect.rect.size.x, value.y);
                _textRect.sizeDelta = new Vector2(_textRect.rect.size.x, value.y);
            }
        }

        MassageSO _massageSO;

        [Inject]
        public void Construct(MassageSO massageSO)
        {
            _massageSO = massageSO;
        }

        public async Task WriteTextAsync(string text)
        {
            await Task.Run(() =>
            {
                UnityMainThreadDispatcher.Instance().Enqueue(async () =>
                {
                    _textField.text = String.Empty;
                    foreach (var ch in text)
                    {
                        _textField.text += ch;
                        await Task.Delay(_massageSO.WriteSpeed);
                    }
                });
            });
        }
        
        public void WriteText(string text)
        {
            _textField.text = String.Empty;
            foreach (var ch in text)
            {
                _textField.text += ch;
            }
        }
    }
}
