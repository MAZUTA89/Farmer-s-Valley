using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Scripts.MainMenuCode
{
    public class GameStatePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI NameTextField;
        public TextMeshProUGUI NameText => NameTextField;
    }
}
