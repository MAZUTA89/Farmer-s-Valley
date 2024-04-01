using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace Scripts.InventoryCode
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] Image IconElement;
        [SerializeField] TextMeshProUGUI TextElement;
        public bool IsEmpty {  get; private set; }

        public Image Icon => IconElement;
        public TextMeshProUGUI Text => TextElement;
        private void Start()
        {
        }

        public void SetEmpty(bool isEmpty)
        {
            IsEmpty = isEmpty;
        }
    }
}
