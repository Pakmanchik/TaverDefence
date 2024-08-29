using TMPro;
using UnityEngine;

namespace Localization.Shared
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class WrapperTMP : MonoBehaviour
    {
        [SerializeField] private string sf_idText;
        [SerializeField] private TextMeshProUGUI sf_textForProcessing;

        private TextSettings _textSettings;

        public string Id
        {
            get => sf_idText;
            set => sf_idText = value;
        }

        public TextSettings TextSettings
        {
            get => _textSettings;
            set
            {
                _textSettings = value;
                SetNewSettings();
            }
        }

        private void SetNewSettings()
        {
            sf_textForProcessing.fontSize = _textSettings.SizeText;
            sf_textForProcessing.text     = _textSettings.Text;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (sf_textForProcessing == null)
                sf_textForProcessing = GetComponent<TextMeshProUGUI>();
        }
#endif
    }
}