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
                _rectTransform.rect.Set(_rectTransform.rect.position.x,
                    _rectTransform.rect.position.y, value.x, value.y);
            }
        }
        public (float, float) WidthHeight => (_rectTransform.rect.size.x, _rectTransform.rect.size.y);

        MassageSO _massageSO;

        [Inject]
        public void Construct(MassageSO massageSO)
        {
            _massageSO = massageSO;
        }

        public async Task WriteText(string text)
        {
            var textField = _textField.text;
            foreach (var ch in text)
            {
                textField += ch;
                await Task.Delay(_massageSO.WriteSpeed);
            }
        }
    }
}
