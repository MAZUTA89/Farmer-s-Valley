using TMPro;
using UnityEngine;

namespace Scripts.MainMenuScripts
{
    public class NewGamePanel : MonoBehaviour
    {
        [SerializeField] private TMP_InputField TMP_InputField;
        public TMP_InputField InputText => TMP_InputField;
        [SerializeField] private TextMeshProUGUI InfoTextField;
        public TextMeshProUGUI InfoText => InfoTextField;
    }
}
