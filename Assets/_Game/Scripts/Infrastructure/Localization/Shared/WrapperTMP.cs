using TMPro;
using UnityEngine;

namespace Localization.Shared
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public sealed class WrapperTMP : MonoBehaviour
    {
        [SerializeField] private LocalizationKeys.GameStrings sf_game;
        [SerializeField] private string sf_idText;
        [SerializeField] private TextMeshProUGUI sf_textForProcessing;
        //[SerializeField] private TextAppearanceAnim sf_appearanceAnim;

        private TextSettings _textSettings;

        public string Id
        {
            get => sf_idText;
            set => sf_idText = value;
        }

        private void OnValidate()
        {
            if (sf_textForProcessing == null)
                sf_textForProcessing = GetComponent<TextMeshProUGUI>();

            sf_idText = LocalizationKeys.ToFriendlyString(sf_game);
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

            /*if (sf_appearanceAnim)
                sf_appearanceAnim.StartAnim(_textSettings.Text);
            else
                sf_textForProcessing.text = _textSettings.Text*/;

        }
    }
}