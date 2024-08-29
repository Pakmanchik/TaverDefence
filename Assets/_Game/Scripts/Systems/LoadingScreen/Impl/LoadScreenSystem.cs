using Localization.Contract;
using Systems.LoadingScreen.Contract;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Systems.LoadingScreen.Impl
{
    public sealed class LoadScreenSystem : MonoBehaviour, ILoadScreenSystem
    {
      //  [SerializeField] private Canvas _canvas;

       // [SerializeField] private Slider _loadingSlider;
        [SerializeField] private TextMeshProUGUI _percentLoadText;

        [Inject] private ILocalizationCollection _localizationCollection;

        public void Show()
        {
          //  _loadingSlider.value = 0;
            Enable();
        }
        
        /// <param name="percent"> от 0 до 100</param>
        public void SetPercent(uint percent)
        {
           // _loadingSlider.value  = percent;
//            _percentLoadText.text = percent.ToString();
        }

        public void Hide()
        {
            Disable();
          //  _loadingSlider.value = 0;
        }

        private void Enable()
        {
          //  _canvas.enabled = true;
        }

        private void Disable()
        {
         //   _canvas.enabled = false;
        }
    }
}